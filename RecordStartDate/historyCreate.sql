SELECT * 
INTO #UPDATE_DATA
FROM(
		SELECT Id,
			Loginname,
			Password,
			RegistrationDate,
			LastLoginDate,
			Charactername,
			Nation,
			Geartype,
			Level,
			Levelpercentage,
			Spi,
			Credits,
			Fame,
			Brigade,
			Attack,
			Defence,
			Evasion,
			Fuel,
			Spirit,
			Shield,
			UnusedStatpoints
		FROM AccountTmp
		Except
		SELECT Id,
			Loginname,
			Password,
			RegistrationDate,
			LastLoginDate,
			Charactername,
			Nation,
			Geartype,
			Level,
			Levelpercentage,
			Spi,
			Credits,
			Fame,
			Brigade,
			Attack,
			Defence,
			Evasion,
			Fuel,
			Spirit,
			Shield,
			UnusedStatpoints
		FROM AccountHistory
		WHERE RecordEndDate = '9999-12-31'
) upd;


--Accounts, bei denen sich Werte geändert haben
UPDATE AccountHistory
SET RecordEndDate = (
						SELECT dateadd(day,-1, max(FileDate))
						FROM game..AccountTmp
					)
WHERE RecordEndDate = '9999-12-31'
AND
Id in
	(
		SELECT Id
		FROM #UPDATE_DATA
	)
;

--Accounts, die gelöscht wurden
UPDATE AccountHistory
SET RecordEndDate = (
						SELECT dateadd(day,-1, max(FileDate))
						FROM game..AccountTmp
					)
WHERE RecordEndDate = '9999-12-31'
AND
Id in
	(
		SELECT Id
		FROM AccountHistory
		WHERE RecordEndDate = '9999-12-31'
		EXCEPT
		SELECT Id
		FROM AccountTmp
	)
;


INSERT INTO AccountHistory (Id, Loginname, Password, RegistrationDate, LastLoginDate, Charactername, Nation, Geartype, Level,
			Levelpercentage, Spi, Credits, Fame, Brigade, Attack, Defence, Evasion, Fuel, Spirit, Shield, UnusedStatpoints)
SELECT  udt.Id,
		dat.RSD,
		'9999-12-31',
		udt.Id,
		udt.Loginname,
		udt.Password,
		udt.RegistrationDate,
		udt.LastLoginDate,
		udt.Charactername,
		udt.Nation,
		udt.Geartype,
		udt.Level,
		udt.Levelpercentage,
		udt.Spi,
		udt.Credits,
		udt.Fame,
		udt.Brigade,
		udt.Attack,
		udt.Defence,
		udt.Evasion,
		udt.Fuel,
		udt.Spirit,
		udt.Shield,
		udt.UnusedStatpoints
FROM #UPDATE_DATA udt,
		(
			SELECT max(FileDate) as RSD
			FROM AccountTmp
		)dat;


--Test
SELECT *
FROM AccountHistory
ORDER BY Id, RecordStartDate;

