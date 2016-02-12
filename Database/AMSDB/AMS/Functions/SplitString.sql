CREATE FUNCTION [AMS].[SplitString]
(
	@Input NVARCHAR(MAX), --input string to be separated
    @Separator NVARCHAR(MAX) = ',', --a string that delimit the substrings in the input string
    @RemoveEmptyEntries BIT = 1 --the return value does not include array elements that contain an empty string
)
RETURNS @TABLE table
(
	[Id] int IDENTITY(1,1),
	[Value] NVARCHAR(MAX)
)
AS
BEGIN
	DECLARE @Index int, @Entry NVARCHAR(max)
	SET @Index = CHARINDEX(@Separator, @Input)
	WHILE (@Index>0)
	BEGIN
		SET @Entry=ltrim(rtrim(substring(@Input, 1, @Index-1)))
		IF (@RemoveEmptyEntries=0) or (@RemoveEmptyEntries = 1 AND @Entry <> '')
		BEGIN
			INSERT INTO @TABLE([Value]) VALUES(@Entry)
		END
		SET @Input = substring(@Input, @Index+datalength(@Separator) / 2, LEN(@Input))
		SET @Index = charindex(@Separator, @Input)
	END

	SET @Entry=ltrim(rtrim(@Input))

	IF (@RemoveEmptyEntries = 0) OR (@RemoveEmptyEntries=1 AND @Entry <> '')
	BEGIN
		INSERT INTO @TABLE([Value]) VALUES (@Entry)
	END
	RETURN
END
