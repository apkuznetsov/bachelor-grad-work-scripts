

To search through tags for exact string equality use this pattern: /^127.0.0.1$/

SELECT * FROM sensor_outputs_test_132 WHERE time > 1588985970717778600 AND sensor_ip =~ /^127.0.0.1$/

SELECT * FROM sensor_outputs_test_132 WHERE time > 1588985970717778600 AND sensor_ip =~ /^127.0.0.1$/ AND sensor_port =~ /^1111$/

SELECT * FROM sensor_outputs_test_132 WHERE time >= 1588985970717778600 AND time <= 1589024162229517700 AND sensor_ip =~ /^127.0.0.1$/ AND sensor_port =~ /^1111$/