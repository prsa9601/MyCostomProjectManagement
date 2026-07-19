select * FROM dbo.Users  as u
where u.FullName = 'Parsa'

SELECT TOP(1) * 
FROM dbo.UserOtpSession AS u
WHERE u.IsActive = 1 
  AND u.ExpireDate > GETDATE()
ORDER BY u.ExpireDate ASC;  -- (اختیاری) اولویت با نزدیک‌ترین تاریخ انقضا


select top 1  * from dbo.UserOtp as o 
order  by o.CreationDate desc
--DELETE FROM Users