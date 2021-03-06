-- "datatypes"
CREATE TABLE datatypes
(
 "DataTypeId" serial NOT NULL,
 "Metadata" text NOT NULL,
 "Schema"   json NOT NULL,
	
 CONSTRAINT "PK_Datatypes" PRIMARY KEY ( "DataTypeId" )
);

-- "communication_protocols"
CREATE TABLE communication_protocols
(
 "CommunicationProtocolId" serial NOT NULL,
 "ProtocolName"            varchar(20) NOT NULL,
	
 CONSTRAINT "PK_CommunicationProtocols" PRIMARY KEY ( "CommunicationProtocolId" )
);

-- "sensors"
CREATE TABLE sensors
(
 "SensorId"                serial NOT NULL,
 "Metadata"                text NOT NULL,
 "DataTypeId"              int NOT NULL,
 "IpAddress"               varchar(50) NOT NULL,
 "Port"                    int NOT NULL,
 "CommunicationProtocolId" int NOT NULL,
	
 CONSTRAINT "PK_Sensors" PRIMARY KEY ( "SensorId" ),
	
 CONSTRAINT "FK_Sensor_DataTypeId_Datatype" FOREIGN KEY ( "DataTypeId" ) 
	REFERENCES datatypes ( "DataTypeId" ),
 CONSTRAINT "FK_Sensor_CommunicationProtocolId_CommunicationProtocol" FOREIGN KEY ( "CommunicationProtocolId" ) 
	REFERENCES communication_protocols ( "CommunicationProtocolId" )
);

-- "experiments"
CREATE TABLE experiments
(
 "ExperimentId" serial NOT NULL,
 "Metadata"     text NOT NULL,
 "CreatedAt"    timestamp with time zone NOT NULL,
	
 CONSTRAINT "PK_Experiments" PRIMARY KEY ( "ExperimentId" )
);

-- "tests"
CREATE TABLE tests
(
 "TestId"       serial NOT NULL,
 "Metadata"     text NOT NULL,
 "ExperimentId" int NOT NULL,
 "StartedTime"  timestamp with time zone NOT NULL DEFAULT (now()),
 "EndedTime"    timestamp with time zone NULL,
	
 CONSTRAINT "PK_Tests" PRIMARY KEY ( "TestId" ),
	
 CONSTRAINT "FK_Test_ExperimentId_Experiment" FOREIGN KEY ( "ExperimentId" ) 
	REFERENCES experiments ( "ExperimentId" )
);

-- "storage_files"
CREATE TABLE storage_files
(
 "StorageFileId" serial NOT NULL,
 "URI"           text NOT NULL,
 "Description"   text NOT NULL,
	
 CONSTRAINT "PK_StorageFiles" PRIMARY KEY ( "StorageFileId" )
);

-- "test_storage_files"
CREATE TABLE test_storage_files
(
 "TestStorageFileId" serial NOT NULL,
 "StorageFileId"     int NOT NULL,
 "TestId"            int NOT NULL,
 "IsInput"       boolean NOT NULL,
	
 CONSTRAINT "PK_TestStorageFiles" PRIMARY KEY ( "TestStorageFileId" ),
	
 CONSTRAINT "FK_TestStorageFile_StorageFileId_StorageFile" FOREIGN KEY ( "StorageFileId" ) 
	REFERENCES storage_files ( "StorageFileId" ),
 CONSTRAINT "FK_TestStorageFile_TestId_Test" FOREIGN KEY ( "TestId" ) 
	REFERENCES tests ( "TestId" )
);

-- "processings"
CREATE TABLE processings
(
 "ProcessingId" serial NOT NULL,
 "Metadata"            text NOT NULL,
 "StartTimeBorder"     timestamp with time zone NOT NULL,
 "EndTimeBorder"       timestamp with time zone NOT NULL,
	
 CONSTRAINT "PK_Processings" PRIMARY KEY ( "ProcessingId" )
);

-- "processing_tests"

CREATE TABLE processing_tests
(
 "ProcessingTestId"    serial NOT NULL,
 "ProcessingId" int NOT NULL,
 "TestId"              int NOT NULL,
	
 CONSTRAINT "PK_ProcessingTests" PRIMARY KEY ( "ProcessingTestId" ),
	
 CONSTRAINT "FK_ProcessingTest_ProcessingId_Processing" 
	FOREIGN KEY ( "ProcessingId" ) 
	REFERENCES processings ( "ProcessingId" ),
 CONSTRAINT "FK_ProcessingTest_TestId_Test" 
	FOREIGN KEY ( "TestId" ) 
	REFERENCES tests ( "TestId" )
);

-- "processing_sensors"
CREATE TABLE processing_sensors
(
 "ProcessingSensorId" serial NOT NULL,
 "ProcessingId"       int NOT NULL,
 "SensorId"           int NOT NULL,
	
 CONSTRAINT "PK_ProcessingsSensors" PRIMARY KEY ( "ProcessingSensorId" ),
	
 CONSTRAINT "FK_ProcessingSensor_ProcessingId_Processing" 
	FOREIGN KEY ( "ProcessingId" ) 
	REFERENCES processings ( "ProcessingId" ),
 CONSTRAINT "FK_ProcessingSensor_SensorId_Sensor" FOREIGN KEY ( "SensorId" ) 
	REFERENCES sensors ( "SensorId" )
);

-- "processed_data"
CREATE TABLE processed_data
(
 "ProcessedDataId"     serial NOT NULL,
 "ProcessingId" int NOT NULL,
 "StorageFileId"       int NOT NULL,
	
 CONSTRAINT "PK_ProcessedData" PRIMARY KEY ( "ProcessedDataId" ),
	
 CONSTRAINT "FK_ProcessedData_ProcessingsId_Processing" 
	FOREIGN KEY ( "ProcessingId" ) 
	REFERENCES processings ( "ProcessingId" ),
 CONSTRAINT "FK_ProcessedData_StorageFileId_StorageFile" 
	FOREIGN KEY ( "StorageFileId" ) 
	REFERENCES storage_files ( "StorageFileId" )
);

-- experiment_sensors
CREATE TABLE experiment_sensors
(
 "ExperimentSensorId" serial NOT NULL,
 "ExperimentId"       int NOT NULL,
 "SensorId"           int NOT NULL,
	
 CONSTRAINT "PK_ExperimentSensors" PRIMARY KEY ( "ExperimentSensorId" ),
	
 CONSTRAINT "FK_ExperimentSensor_ExperimentId_Experiment" FOREIGN KEY ( "ExperimentId" ) 
	REFERENCES experiments ( "ExperimentId" ),
 CONSTRAINT "FK_ExperimentSensor_SensorId_Sensor" FOREIGN KEY ( "SensorId" ) 
	REFERENCES sensors ( "SensorId" )
);

-- "tags"
CREATE TABLE tags
(
 "TagId" serial NOT NULL,
 "Value" text NOT NULL,
	
 CONSTRAINT "PK_Tags" PRIMARY KEY ( "TagId" )
);

-- "experiment_tags"
CREATE TABLE experiment_tags
(
 "ExperimentTagId" serial NOT NULL,
 "ExperimentId"    int NOT NULL,
 "TagId"           int NOT NULL,
	
 CONSTRAINT "PK_ExperimentTags" PRIMARY KEY ( "ExperimentTagId" ),
	
 CONSTRAINT "FK_ExperimentTag_ExperimentId_Experiment" FOREIGN KEY ( "ExperimentId" ) 
	REFERENCES experiments ( "ExperimentId" ),
 CONSTRAINT "FK_ExperimentTag_TadId_Tag" FOREIGN KEY ( "TagId" ) 
	REFERENCES tags ( "TagId" )
);

-- "metadata_parameters"
CREATE TABLE metadata_parameters
(
 "MetadataParameterId" serial NOT NULL,
 "Name"                text NOT NULL,
 "Value"               text NOT NULL,
	
 CONSTRAINT "PK_MetadataParameters" PRIMARY KEY ( "MetadataParameterId" )
);

-- "experiment_params"
CREATE TABLE experiment_params
(
 "ExperimentParamId"   serial NOT NULL,
 "ExperimentId"        int NOT NULL,
 "MetadataParameterId" int NOT NULL,
	
 CONSTRAINT "PK_ExperimentParams" PRIMARY KEY ( "ExperimentParamId" ),
	
 CONSTRAINT "FK_ExperimentParam_ExperimentId_Experiment" FOREIGN KEY ( "ExperimentId" ) 
	REFERENCES experiments ( "ExperimentId" ),
 CONSTRAINT "FK_ExperimentParam_MetadataParameterId_MetadataParameter" FOREIGN KEY ( "MetadataParameterId" ) 
	REFERENCES metadata_parameters ( "MetadataParameterId" )
);

-- "test_params"
CREATE TABLE dms_v9.test_params
(
 "TestParamId"         serial NOT NULL,
 "TestId"              int NOT NULL,
 "MetadataParameterId" int NOT NULL,
	
 CONSTRAINT "PK_TestParams" PRIMARY KEY ( "TestParamId" ),
	
 CONSTRAINT "FK_TestParam_TestId_Test" FOREIGN KEY ( "TestId" ) 
	REFERENCES tests ( "TestId" ),
 CONSTRAINT "FK_TestParam_MetadataParameterId_MetadataParameter" FOREIGN KEY ( "MetadataParameterId" ) 
	REFERENCES metadata_parameters ( "MetadataParameterId" )
);

-- adding communication protocols
INSERT INTO dms_v9.communication_protocols("ProtocolName")
	VALUES 
	('TCP'),
	('UDP'),
	('MQTT');