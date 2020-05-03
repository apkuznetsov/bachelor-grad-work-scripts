CREATE SCHEMA IF NOT EXISTS dms_v8;

-- ************************************** "datatypes"
CREATE TABLE dms_v8.datatypes
(
 "DataTypeId" int NOT NULL,
 "Metadata" text NOT NULL,
 "Schema"   jsonb NOT NULL,
	
 CONSTRAINT "PK_Datatypes" PRIMARY KEY ( "DataTypeId" )
);

-- ************************************** "sensors"
CREATE TABLE dms_v8.sensors
(
 "SensorId"   int NOT NULL,
 "Metadata"   text NOT NULL,
 "DataTypeId" int NOT NULL,
	
 CONSTRAINT "PK_Sensors" PRIMARY KEY ( "SensorId" ),
	
 CONSTRAINT "FK_Sensor_DataTypeId_Datatype" FOREIGN KEY ( "DataTypeId" ) 
	REFERENCES dms_v8.datatypes ( "DataTypeId" )
);

-- ************************************** "experiments"
CREATE TABLE dms_v8.experiments
(
 "ExperimentId" int NOT NULL,
 "Metadata"     text NOT NULL,
 "CreatedAt"    time with time zone NOT NULL,
	
 CONSTRAINT "PK_Experiments" PRIMARY KEY ( "ExperimentId" )
);

-- ************************************** "tests"
CREATE TABLE dms_v8.tests
(
 "TestId"       int NOT NULL,
 "Metadata"     text NOT NULL,
 "ExperimentId" int NOT NULL,
 "StartedTime"  timestamp with time zone NOT NULL DEFAULT (now()),
 "EndedTime"    timestamp with time zone NULL,
	
 CONSTRAINT "PK_Tests" PRIMARY KEY ( "TestId" ),
	
 CONSTRAINT "FK_Test_ExperimentId_Experiment" FOREIGN KEY ( "ExperimentId" ) 
	REFERENCES dms_v8.experiments ( "ExperimentId" )
);

-- ************************************** "processings"
CREATE TABLE dms_v8.processings
(
 "ProcessingId"    int NOT NULL,
 "Metadata"        text NOT NULL,
 "TestId"          int NOT NULL,
 "StartTimeBorder" time with time zone NOT NULL,
 "EndTimeBorder"   time with time zone NOT NULL,
	
 CONSTRAINT "PK_Processings" PRIMARY KEY ( "ProcessingId" ),
	
 CONSTRAINT "FK_Processing_TestId_Test" FOREIGN KEY ( "TestId" ) 
	REFERENCES dms_v8.tests ( "TestId" )
);

-- ************************************** "processed_data"
CREATE TABLE dms_v8.processed_data
(
 "ProcessedDataId" int NOT NULL,
 "Metadata"        text NOT NULL,
 "ProcessingId"    int NOT NULL,
 "URI"             text NOT NULL,
	
 CONSTRAINT "PK_ProcessedData" PRIMARY KEY ( "ProcessedDataId" ),
	
 CONSTRAINT "FK_ProcessedData_ProcessingId_Processing" FOREIGN KEY ( "ProcessingId" ) 
	REFERENCES dms_v8.processings ( "ProcessingId" )
);

-- ************************************** "processing_sensors"
CREATE TABLE dms_v8.processing_sensors
(
 "ProcessingSensorId" int NOT NULL,
 "ProcessingId"       int NOT NULL,
 "SensorId"           int NOT NULL,
	
 CONSTRAINT "PK_ProcessingSensors" PRIMARY KEY ( "ProcessingSensorId" ),
	
 CONSTRAINT "FK_ProcessingSensor_ProcessingId_Processing" FOREIGN KEY ( "ProcessingId" ) 
	REFERENCES dms_v8.processings ( "ProcessingId" ),
 CONSTRAINT "FK_ProcessingSensor_SensorId_Sensor" FOREIGN KEY ( "SensorId" ) 
	REFERENCES dms_v8.sensors ( "SensorId" )
);

-- ************************************** experiment_sensors
CREATE TABLE dms_v8.experiment_sensors
(
 "ExperimentId"       int NOT NULL,
 "ExperimentSensorId" int NOT NULL,
 "SensorId"           int NOT NULL,
	
 CONSTRAINT "PK_ExperimentSensors" PRIMARY KEY ( "ExperimentSensorId" ),
	
 CONSTRAINT "FK_ExperimentSensor_ExperimentId_Experiment" FOREIGN KEY ( "ExperimentId" ) 
	REFERENCES dms_v8.experiments ( "ExperimentId" ),
 CONSTRAINT "FK_ExperimentSensor_SensorId_Sensor" FOREIGN KEY ( "SensorId" ) 
	REFERENCES dms_v8.sensors ( "SensorId" )
);

-- ************************************** "tags"
CREATE TABLE dms_v8.tags
(
 "TagId" int NOT NULL,
 "Value" text NOT NULL,
	
 CONSTRAINT "PK_Tags" PRIMARY KEY ( "TagId" )
);

-- ************************************** "experiment_tags"
CREATE TABLE dms_v8.experiment_tags
(
 "ExperimentTagId" int NOT NULL,
 "ExperimentId"    int NOT NULL,
 "TagId"           int NOT NULL,
	
 CONSTRAINT "PK_ExperimentTags" PRIMARY KEY ( "ExperimentTagId" ),
	
 CONSTRAINT "FK_ExperimentTag_ExperimentId_Experiment" FOREIGN KEY ( "ExperimentId" ) 
	REFERENCES dms_v8.experiments ( "ExperimentId" ),
 CONSTRAINT "FK_ExperimentTag_TadId_Tag" FOREIGN KEY ( "TagId" ) 
	REFERENCES dms_v8.tags ( "TagId" )
);

-- ************************************** "metadata_parameters"
CREATE TABLE dms_v8.metadata_parameters
(
 "MetadataParameterId" int NOT NULL,
 "Name"                text NOT NULL,
 "Value"               text NOT NULL,
	
 CONSTRAINT "PK_MetadataParameters" PRIMARY KEY ( "MetadataParameterId" )
);

-- ************************************** "experiment_params"
CREATE TABLE dms_v8.experiment_params
(
 "ExperimentParamId"   int NOT NULL,
 "ExperimentId"        int NOT NULL,
 "MetadataParameterId" int NOT NULL,
	
 CONSTRAINT "PK_ExperimentParams" PRIMARY KEY ( "ExperimentParamId" ),
	
 CONSTRAINT "FK_ExperimentParam_ExperimentId_Experiment" FOREIGN KEY ( "ExperimentId" ) 
	REFERENCES dms_v8.experiments ( "ExperimentId" ),
 CONSTRAINT "FK_ExperimentParam_MetadataParameterId_MetadataParameter" FOREIGN KEY ( "MetadataParameterId" ) 
	REFERENCES dms_v8.metadata_parameters ( "MetadataParameterId" )
);

-- ************************************** "test_params"
CREATE TABLE dms_v8.test_params
(
 "TestParamId"         int NOT NULL,
 "TestId"              int NOT NULL,
 "MetadataParameterId" int NOT NULL,
	
 CONSTRAINT "PK_TestParams" PRIMARY KEY ( "TestParamId" ),
	
 CONSTRAINT "FK_TestParam_TestId_Test" FOREIGN KEY ( "TestId" ) 
	REFERENCES dms_v8.tests ( "TestId" ),
 CONSTRAINT "FK_TestParam_MetadataParameterId_MetadataParameter" FOREIGN KEY ( "MetadataParameterId" ) 
	REFERENCES dms_v8.metadata_parameters ( "MetadataParameterId" )
);

-- ************************************** "storage_files"
CREATE TABLE dms_v8.storage_files
(
 "StorageFileId" int NOT NULL,
 "URI"           text NOT NULL,
 "Description"   text NOT NULL,
 "IsInput"       boolean NOT NULL,
	
 CONSTRAINT "PK_StorageFiles" PRIMARY KEY ( "StorageFileId" )
);

-- ************************************** "test_storage_files"
CREATE TABLE dms_v8.test_storage_files
(
 "TestStorageFileId" int NOT NULL,
 "StorageFileId"     int NOT NULL,
 "TestId"            int NOT NULL,
	
 CONSTRAINT "PK_TestStorageFiles" PRIMARY KEY ( "TestStorageFileId" ),
	
 CONSTRAINT "FK_TestStorageFile_StorageFileId_StorageFile" FOREIGN KEY ( "StorageFileId" ) 
	REFERENCES dms_v8.storage_files ( "StorageFileId" ),
 CONSTRAINT "FK_TestStorageFile_TestId_Test" FOREIGN KEY ( "TestId" ) 
	REFERENCES dms_v8.tests ( "TestId" )
);

-- ********* THIS ONE APPARENTLY WILL BE MOVED TO THE Time Series DB (TSDB)
/*
-- ************************************** "sensor_outputs"
CREATE TABLE dms_v8.sensor_outputs
(
 "SensorId"    int NOT NULL,
 "RawData"     bytea NOT NULL,
 "CollectedAt" timestamp with time zone NOT NULL,
	
 CONSTRAINT "FK_SensorOutputs_SensorId_Sensor" FOREIGN KEY ( "SensorId" ) 
	REFERENCES dms_v8.sensors ( "SensorId" )
);
*/
