CREATE DATABASE [band_tracker_test]
GO
USE [band_tracker_test]
GO
CREATE TABLE [venues] (
  [id] int IDENTITY (1, 1) ,
  [name] varchar(255) ,
  [size] varchar(25) ,
  [capacity] int , 
 PRIMARY KEY ([id])
) ON [PRIMARY]
GO

CREATE TABLE [bands] (
  [id] int IDENTITY (1, 1) ,
  [name] varchar(255) ,
  [number_members] int , 
 PRIMARY KEY ([id])
) ON [PRIMARY]
GO

CREATE TABLE [genres] (
  [id] int IDENTITY (1, 1) ,
  [name] varchar(255) , 
 PRIMARY KEY ([id])
) ON [PRIMARY]
GO

CREATE TABLE [performances] (
  [id] int IDENTITY (1, 1) ,
  [band_id] int ,
  [venue_id] int , 
 PRIMARY KEY ([id])
) ON [PRIMARY]
GO

CREATE TABLE [genres_venues] (
  [id] int IDENTITY (1, 1) ,
  [genre_id] int ,
  [venue_id] int , 
 PRIMARY KEY ([id])
) ON [PRIMARY]
GO

CREATE TABLE [bands_genres] (
  [id] int IDENTITY (1, 1) ,
  [band_id] int ,
  [genre_id] int , 
 PRIMARY KEY ([id])
) ON [PRIMARY]
GO

ALTER TABLE [performances] ADD FOREIGN KEY (band_id) REFERENCES [bands] ([id]);
				
ALTER TABLE [performances] ADD FOREIGN KEY (venue_id) REFERENCES [venues] ([id]);
				
ALTER TABLE [genres_venues] ADD FOREIGN KEY (genre_id) REFERENCES [genres] ([id]);
				
ALTER TABLE [genres_venues] ADD FOREIGN KEY (venue_id) REFERENCES [venues] ([id]);
				
ALTER TABLE [bands_genres] ADD FOREIGN KEY (band_id) REFERENCES [bands] ([id]);
				
ALTER TABLE [bands_genres] ADD FOREIGN KEY (genre_id) REFERENCES [genres] ([id]);