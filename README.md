# PChouse PT Postal Code API

---  

## An API to get portuguese address of postal code, postal code off address, districts and counties  
  
### API to get districts

```javascript

const options = {
	"method": "GET",
	"headers": {
		"Content-Type": "application/json"
	}
}

// Get all
const fetch = fetch("http://localhost:5000/district", options);

// Get a single
const fetch = fetch("http://localhost:5000/district/09", options);

```

### API to get counties

```javascript

// Get all 

const fetch = fetch("http://localhost:5000/county", options);

// Get all of a district

const fetch = fetch("http://localhost:5000/county/09", options);

// Get single

const fetch = fetch("http://localhost:5000/county/09/01", options);

``` 


### API to get address information of an postal code

```javascript

// Get single

const fetch = fetch("http://localhost:5000/postalcode/1100/003", options);

// All api routes

// /postalcode/{pc4:regex(^[0-9]{{4}}$)}/{pc3:regex(^[0-9]{{3}}$)}
// /postalcode/search/{pc4:regex(^[0-9]{{1,4}}|%$)}/{pc3:regex(^[0-9]{{1,3}}|%$)?}/{limit:int?}/{offset:int?}

```

### API address

```javascript

const fetch = fetch("http://localhost:5000/address/contains/", options);

// All api routes

// /address/{type:regex(^(contains|start)$)}/{street}/{pc4:regex(^[0-9]{{2}}|%$)?}/{limit?}/{offset?}

// Post TabulatorRequest to
// /address/tabulator/scroll

```


#### The maximum number of rows by default can be set in appsettings.json  

---  

License MIT

Copyright 2024 Reflexão, Sistemas e Estudos Informáticos, Lda

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
documentation files (the "Software"), to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or
substantial portions of the Software.

#### THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
#### INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
#### IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
#### WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR
#### THE USE OR OTHER DEALINGS IN THE SOFTWARE.
--- 

#### Art made by a Joseon Chinnampo soul for smart people