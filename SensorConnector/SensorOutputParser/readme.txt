

To search through tags for exact string equality use this pattern: /^127.0.0.1$/

SELECT * FROM sensor_outputs_test_132 WHERE time > 1588985970717778600 AND sensor_ip =~ /^127.0.0.1$/

SELECT * FROM sensor_outputs_test_132 WHERE time > 1588985970717778600 AND sensor_ip =~ /^127.0.0.1$/ AND sensor_port =~ /^1111$/

SELECT * FROM sensor_outputs_test_132 WHERE time >= 1588985970717778600 AND time <= 1589024162229517700 AND sensor_ip =~ /^127.0.0.1$/ AND sensor_port =~ /^1111$/

SELECT * FROM sensor_outputs_test_132 WHERE time >= 1588985973804227400 AND time <= 1589024162229517700 AND sensor_ip =~ /^127.0.0.1$/ AND sensor_port =~ /^1111$/ OR sensor_ip =~ /^127.0.0.1$/ AND sensor_port =~ /^3333$/


	DATATYPE SCHEMA

Pattern:
	{
    	   "{fieldName}": "{fieldTypeName}",
    	   "{fieldName}": "{fieldTypeName}",
		...
    	   "{fieldName}": "{fieldTypeName}",
	}

Allowed field type names: 

		int
		double
		string
		bool


	SENSOR BROADCAST

Sensor fields are separated with comma.

Pattern: 
	{fieldValue1},{fieldValue2}, ... ,{fieldValueN}


Example:

	Schema:

	{
    	   "Temperature": "double",
    	   "Moisture": "int",
    	   "IsComfortLevel": "bool",
    	   "Comment": "string"
	}
	
	Sensor Broadcast:
	
	25.4,74,true,Go outside!!!

	OR

	25.4,74,1,Go outside!!!



