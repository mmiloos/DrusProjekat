
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/29/2017 22:33:48
-- Generated from EDMX file: C:\Users\MiloS\Desktop\MeasureService\MeasureService\Model1.edmx
-- --------------------------------------------------
create database DrusNov;
SET QUOTED_IDENTIFIER OFF;
GO
USE [DrusNov];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_LocationMeasuringStation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stations] DROP CONSTRAINT [FK_LocationMeasuringStation];
GO
IF OBJECT_ID(N'[dbo].[FK_MeasuringStationMeasurement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Measures] DROP CONSTRAINT [FK_MeasuringStationMeasurement];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Locations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locations];
GO
IF OBJECT_ID(N'[dbo].[Measures]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Measures];
GO
IF OBJECT_ID(N'[dbo].[Stations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Locations'
CREATE TABLE [dbo].[Locations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Measures'
CREATE TABLE [dbo].[Measures] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Value] float  NOT NULL,
    [Time] datetime  NOT NULL,
    [Station_Id] int  NOT NULL
);
GO

-- Creating table 'Stations'
CREATE TABLE [dbo].[Stations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Location_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [PK_Locations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Measures'
ALTER TABLE [dbo].[Measures]
ADD CONSTRAINT [PK_Measures]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Stations'
ALTER TABLE [dbo].[Stations]
ADD CONSTRAINT [PK_Stations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Location_Id] in table 'Stations'
ALTER TABLE [dbo].[Stations]
ADD CONSTRAINT [FK_LocationMeasuringStation]
    FOREIGN KEY ([Location_Id])
    REFERENCES [dbo].[Locations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationMeasuringStation'
CREATE INDEX [IX_FK_LocationMeasuringStation]
ON [dbo].[Stations]
    ([Location_Id]);
GO

-- Creating foreign key on [Station_Id] in table 'Measures'
ALTER TABLE [dbo].[Measures]
ADD CONSTRAINT [FK_MeasuringStationMeasurement]
    FOREIGN KEY ([Station_Id])
    REFERENCES [dbo].[Stations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MeasuringStationMeasurement'
CREATE INDEX [IX_FK_MeasuringStationMeasurement]
ON [dbo].[Measures]
    ([Station_Id]);
GO

insert into Locations(Name) values('Satelit');
insert into Locations(Name) values('Centar');
insert into Locations(Name) values('Telep');
insert into Stations(Location_Id,Name) values(1,'SatX');
insert into Stations(Location_Id,Name) values(1,'SatY');
insert into Stations(Location_Id,Name) values(2,'Centar1');
insert into Stations(Location_Id,Name) values(3,'Telep1');

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------