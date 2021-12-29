
--WITH KOMUTU
--Karmaþýk bir sorguyu With in içine atarak ve isim vererek ,Tablo gibi kullanmaya yarar.
--GetUserNameAndRole isminde bir tablo var ve bunun getireceði deðer parantez içinde oluþturdugun sorgu olur.
WITH GetUserNameAndRole(Id,Name,Role)
as
(

Select u.Id,u.Firstname,r.RoleName from  [User] u inner JOIN UserRoleMap urm on u.Id=urm.UserId
						INNER JOIN [Role] r  ON r.Id=urm.RoleId
							WHere u.Id=13
)

--GetUserNameAndRole tablo gibi kullandýk
Select * from	GetUserNameAndRole gu INNER JOIN UserFavoritePage ufp ON gu.Id=ufp.UserId


		