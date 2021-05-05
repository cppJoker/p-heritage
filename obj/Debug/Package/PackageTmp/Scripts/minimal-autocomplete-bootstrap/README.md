# bootstrap-autocomplete
**See <a href="https://vigneshksaithal.github.io/bootstrap-autocomplete/demo.html">DEMO</a>**
## Features
- **1.8kb minified**
- **Pure Vanilla JavaScript**
- **Only bootstrap CSS Required**

## Installation
Fork this project and use it in your website.
<a id="raw-url" href="https://github.com/vigneshksaithal/bootstrap-autocomplete">Fork It</a>

[OR]

You can also use this by adding the below code to your website[For testing/demo purposes].
```
<script src="https://cdn.jsdelivr.net/gh/vigneshksaithal/bootstrap-autocomplete@1.2/src/index.min.js"></script>
```
## Usage

STEP 0: Add bootstrap CSS.

STEP 1: Add javascript to your web app.
```
<script src="/bootstrap-autocomplete.min.js"></script>
```

[OR]

Directly add jsDelivr CDN provided below.
```
<script src="https://cdn.jsdelivr.net/gh/vigneshksaithal/bootstrap-autocomplete@main/src/index.min.js"></script>
```

STEP 2: Add HTML code.
Give the component div **id="autocomplete"**.

```
<div id="autocomplete">
  <input id="search" type="text" class="form-contorol"/>
</div>
```

STEP 3: Initiate the autocomplete function.
Pass the search(input) element and the array of values.

```
<script>
  var arr = ['apple', 'mango', 'grapes'];
  var search = document.getElementById('search');
  autocomplete(search, arr);
</script>
```

## Contributions
If you find any bugs or have any feature requests then just raise a issue.
Thanks You🙏
