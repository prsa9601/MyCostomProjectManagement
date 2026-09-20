use CustomProjectManagement
select top(1) * from UserOtp u
order by u.CreationDate desc

select top(1) * from UserOtpSession u
order by u.CreationDate desc

select * from Users u
order by u.CreationDate desc