DELETE FROM game..AccountHistory
WHERE RecordStartDate >= 
	(
		SELECT max(FileDate)
		FROM game..AccountTmp
	);

UPDATE game..AccountHistory
SET RecordEndDate = '9999-12-31'
WHERE RecordEndDate = (
						SELECT dateadd(day,-1, max(FileDate))
						FROM game..AccountTmp
					);
