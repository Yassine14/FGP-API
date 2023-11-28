USE [FGP-Dev]
GO

DELETE FROM [dbo].[AspNetUserTokens]
      WHERE  LoginProvider='FGPFront'
GO

DELETE FROM [FGP-Dev].[dbo].[AspNetUsers]
 WHERE EmailConfirmed= 0
 GO

 DELETE FROM [FGP-Dev].[dbo].[AppUsers]
 WHERE IsCompany= 0
 GO

 