Feature: Cache
![Cache](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Cache by key value
	
Link to a feature: [Calculator]($projectname$/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@cache
Scenario: Cache by key
	Given the key is 'key'
	And the value is 'value'
	When set cache
	Then the result should be success