
--WITH KOMUTU
--Karma��k bir sorguyu With in i�ine atarak ve isim vererek ,Tablo gibi kullanmaya yarar.
--GetUserNameAndRole isminde bir tablo var ve bunun getirece�i de�er parantez i�inde olu�turdugun sorgu olur.
WITH GetUserNameAndRole(Id,Name,Role)
as
(

Select u.Id,u.Firstname,r.RoleName from  [User] u inner JOIN UserRoleMap urm on u.Id=urm.UserId
						INNER JOIN [Role] r  ON r.Id=urm.RoleId
							WHere u.Id=13
)

--GetUserNameAndRole tablo gibi kulland�k
Select * from	GetUserNameAndRole gu INNER JOIN UserFavoritePage ufp ON gu.Id=ufp.UserId


		