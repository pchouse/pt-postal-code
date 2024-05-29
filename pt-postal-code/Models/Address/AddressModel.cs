using PChouse.PTPostalCode.Tabulator;
using System.Data.Entity;

namespace PChouse.PTPostalCode.Models.Address;

public class AddressModel(NHibernate.IStatelessSession _session)
{   

    public List<AddressEntity> Search(AddressSearchType searchType, string street, int limit, int offset, string? pc4)
    {
        var query = _session.Query<AddressEntity>();

        query = searchType == AddressSearchType.contains ?
                query.Where(a => a.Street.Contains(street)) :
                query.Where(a => a.Street.StartsWith(street));

        if (!string.IsNullOrEmpty(pc4))
        {
            query = query.Where(a => a.Pc4.StartsWith(pc4));
        }

        return [.. query.Take(limit).Skip(offset)];
    }

    /// <summary>
    /// Get Tabulator response data for progressive load type scroll
    /// </summary>
    /// <param name="tabulatorRequest"></param>
    /// <returns></returns>
    public async Task<TabulatorResponse<AddressEntity>> TabulatorDataScrollAsync(TabulatorRequest tabulatorRequest)
    {
        
            var response = new TabulatorResponse<AddressEntity>();

            if(tabulatorRequest.Size <= 0) return response;

            var criterions = this.CreateCriterions(tabulatorRequest.Filter);

            var offset = tabulatorRequest.Size * (tabulatorRequest.Page - 1);

            response.Data = await this.TabulatorDataAsync(criterions, tabulatorRequest.Sort, tabulatorRequest.Size, offset);
            
            var numberOfRows = await this.CountTabultaorPaginatorRowAsync(criterions);

            response.LstaPage = (int)Math.Ceiling((double)(numberOfRows / tabulatorRequest.Size));

            return response;
        
    }

    /// <summary>
    /// Number of rows in table for tabulator filters
    /// </summary>
    /// <param name="criterions"></param>
    /// <returns></returns>
    public Task<int> CountTabultaorPaginatorRowAsync(List<NHibernate.Criterion.ICriterion> criterions)
    {
        var query = _session.CreateCriteria<AddressEntity>();
        query.SetProjection(NHibernate.Criterion.Projections.RowCount());
        criterions.ForEach(c => query.Add(c));
        return query.UniqueResultAsync<int>();
    }

    /// <summary>
    /// Get the request tabulator data
    /// </summary>
    /// <param name="criterions"></param>
    /// <param name="sorts"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public Task<IList<AddressEntity>> TabulatorDataAsync(List<NHibernate.Criterion.ICriterion> criterions, List<TabulatorRequestSort> sorts, int limit, int offset)
    {
        var query = _session.CreateCriteria<AddressEntity>();
        criterions.ForEach(criteron => query.Add(criteron));
        sorts.ForEach(s => query.AddOrder(
            new NHibernate.Criterion.Order(s.Field, string.IsNullOrEmpty(s.Dir) || !s.Dir.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
        ));

        query.SetFirstResult(offset).SetMaxResults(limit);

        return query.ListAsync<AddressEntity>();
    }

    /// <summary>
    /// Create the sql query filter
    /// </summary>
    /// <param name="filters"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public List<NHibernate.Criterion.ICriterion> CreateCriterions(List<TabulatorRequestFilter> filters)
    {
        var criterion = new List<NHibernate.Criterion.ICriterion>();

        filters.ForEach(filter =>
        {
            var fieldInfo = typeof(AddressEntity).GetProperty(filter.Field) ?? throw new Exception($"Field {filter.Field} not exist");

            var isNulable = Nullable.GetUnderlyingType(fieldInfo.GetType()) != null;

            if (string.IsNullOrEmpty(filter.Value))
            {
                if (filter.Type == "=")
                {
                    if (isNulable)
                    {
                        criterion.Add(NHibernate.Criterion.Restrictions.IsNull(filter.Field));
                    }
                }

                if (filter.Type == "!=")
                {
                    if (isNulable)
                    {
                        criterion.Add(NHibernate.Criterion.Restrictions.IsNotNull(filter.Field));
                    }
                    else
                    {
                        criterion.Add(NHibernate.Criterion.Restrictions.Not(
                            NHibernate.Criterion.Restrictions.Eq(filter.Field, "")
                        ));
                    }
                    return;
                }

                criterion.Add(NHibernate.Criterion.Restrictions.Eq(filter.Field, ""));
                return;
            }

            object value = filter.Value;

            if (fieldInfo.GetType() == typeof(int))
            {
                if (int.TryParse(filter.Value, out int result))
                {
                    value = result;
                }
            }


            if (filter.Type == "=")
            {
                criterion.Add(NHibernate.Criterion.Restrictions.Eq(filter.Field, value));
                return;
            }
            
            if (filter.Type == "!=")
            {
                criterion.Add(
                    NHibernate.Criterion.Restrictions.Not(
                        NHibernate.Criterion.Restrictions.Eq(filter.Field, value)
                    )
               );
                return;
            }

            if (filter.Type == "like")
            {
                criterion.Add(NHibernate.Criterion.Restrictions.Like(filter.Field, $"%{value}%"));
                return;
            }

            if (filter.Type == "starts")
            {
                criterion.Add(NHibernate.Criterion.Restrictions.Like(filter.Field, $"{value}%"));
                return;
            }

            if (filter.Type == "ends")
            {
                criterion.Add(NHibernate.Criterion.Restrictions.Like(filter.Field, $"%{value}"));
                return;
            }

            if (filter.Type == "<")
            {
                criterion.Add(NHibernate.Criterion.Restrictions.Lt(filter.Field, value));
                return;
            }

            if (filter.Type == "<=")
            {
                criterion.Add(NHibernate.Criterion.Restrictions.Le(filter.Field, value));
                return;
            }

            if (filter.Type == ">")
            {
                criterion.Add(NHibernate.Criterion.Restrictions.Gt(filter.Field, value));
                return;
            }

            if (filter.Type == ">=")
            {
                criterion.Add(NHibernate.Criterion.Restrictions.Ge(filter.Field, value));
                return;
            }

            throw new Exception($"Unknow or not implemented filter type {filter.Type}");

        });

        return criterion;
    }

}
