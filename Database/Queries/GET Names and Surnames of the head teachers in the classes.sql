SELECT Klasy.IdKlasy,Klasy.Oddział,Nauczyciele.Imię,Nauczyciele.Nazwisko 
FROM [KursyELearningDB].[dbo].[Klasy],[KursyELearningDB].[dbo].[Nauczyciele] 
WHERE Klasy.IdWychowawcy = Nauczyciele.IdProwadzącego