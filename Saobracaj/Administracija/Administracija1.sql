-- Modul "Administracija" u NewMain (dugme btnAdministracija, forma Saobracaj.Administracija.Administracija1)
-- Upisuje modul u MainNovi i kartice "Korisnici" i "Prava" u mainNoviKartice.
-- Prava korisnicima se zatim dodeljuju kroz formu Prava (AdministracijaPravoPristupa).
-- Nazivi moraju biti isti kao tekst dugmadi: modul = "Administracija", kartice = "Korisnici" i "Prava".

DECLARE @MainID int;

SELECT @MainID = ID FROM dbo.MainNovi WHERE LOWER(RTRIM(Naziv)) = N'administracija';

IF @MainID IS NULL
BEGIN
    IF COLUMNPROPERTY(OBJECT_ID('dbo.MainNovi'), 'ID', 'IsIdentity') = 1
    BEGIN
        INSERT INTO dbo.MainNovi (Naziv) VALUES (N'Administracija');
        SET @MainID = CAST(SCOPE_IDENTITY() AS int);
    END
    ELSE
    BEGIN
        SELECT @MainID = ISNULL(MAX(ID), 0) + 1 FROM dbo.MainNovi;
        INSERT INTO dbo.MainNovi (ID, Naziv) VALUES (@MainID, N'Administracija');
    END
END

IF NOT EXISTS (SELECT 1 FROM dbo.mainNoviKartice WHERE MainID = @MainID AND Parent IS NULL AND RTRIM(Naziv) = N'Korisnici')
    INSERT INTO dbo.mainNoviKartice (MainID, Parent, Naziv) VALUES (@MainID, NULL, N'Korisnici');

IF NOT EXISTS (SELECT 1 FROM dbo.mainNoviKartice WHERE MainID = @MainID AND Parent IS NULL AND RTRIM(Naziv) = N'Prava')
    INSERT INTO dbo.mainNoviKartice (MainID, Parent, Naziv) VALUES (@MainID, NULL, N'Prava');

SELECT @MainID AS MainID_Administracija;
SELECT ID, MainID, Parent, Naziv FROM dbo.mainNoviKartice WHERE MainID = @MainID;
GO
