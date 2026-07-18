select * FROM dbo.Users  as u

select top 1  * from dbo.UserOtp as o 
order  by o.CreationDate desc
--DELETE FROM Users