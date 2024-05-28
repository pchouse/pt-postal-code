using log4net;
using NHibernate;

namespace PChouse.PTPostalCode.Database;

public class Logger: INHibernateLogger
{

    private static readonly ILog log = LogManager.GetLogger(typeof(NHibernateLogger));

    public void Log(NHibernateLogLevel logLevel, NHibernateLogValues state, Exception exception)
    {
        switch (logLevel)
        {
            case NHibernateLogLevel.Trace:
            case NHibernateLogLevel.Debug:
                log.Debug(state.Format, exception);
                break;
            case NHibernateLogLevel.Info:
                log.Info(state.Format, exception);
                break;
            case NHibernateLogLevel.Warn:
                log.Warn(state.Format, exception);
                break;
            case NHibernateLogLevel.Error:
                log.Error(state.Format, exception);
                break;
            case NHibernateLogLevel.Fatal:
                log.Fatal(state.Format, exception);
                break;
        }
    }

    public bool IsEnabled(NHibernateLogLevel logLevel) => log.IsDebugEnabled;
}
