BEGIN TRANSACTION

UPDATE park
SET latitude = 41.2412,
longitude = -81.5480
WHERE parkCode = 'CVNP'

UPDATE park
SET latitude = 25.4687,
longitude = -80.4775
WHERE parkCode = 'ENP'

UPDATE park
SET latitude = 36.2142,
longitude = -112.5901
WHERE parkCode = 'GCNP'

UPDATE park
SET latitude = 48.5022,
longitude = -113.9885
WHERE parkCode = 'GNP'

UPDATE park
SET latitude = 35.7026,
longitude = -83.3857
WHERE parkCode = 'GSMNP'

UPDATE park
SET latitude = 43.7542,
longitude = -111.0314
WHERE parkCode = 'GTNP'

UPDATE park
SET latitude = 46.8546,
longitude = -121.7928
WHERE parkCode = 'MRNP'

UPDATE park
SET latitude = 41.2412,
longitude = -81.5480
WHERE parkCode = 'RMNP'

UPDATE park
SET latitude = 41.2412,
longitude = -81.5480
WHERE parkCode = 'YNP'

UPDATE park
SET latitude = 41.2412,
longitude = -81.5480
WHERE parkCode = 'YNP2'


SELECT *
FROM park

ROLLBACK TRANSACTION

SELECT *
FROM park