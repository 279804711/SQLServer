select s.UserName, s.PassWord, s.NickName, s.Gender, u.Chinese, u.English, u.Math, u.RecordTime from Table_1 as s
left join
UserScoresT as u
on s.UserName = u.UserName


select s.UserName, s.PassWord, s.NickName, s.Gender, u.Chinese, u.English, u.Math, u.RecordTime from Table_1 as s
left join
(select UserScoresT.* from UserScoresT inner join
(select UserName,Max(RecordTime) as RecordTime from UserScoresT
group by UserName) as groupt
on UserScoresT.UserName = groupt.UserName and UserScoresT.RecordTime = groupt.RecordTime) as u
on s.UserName = u.UserName

select UserScoresT.* from UserScoresT inner join
(select UserName,Max(RecordTime) as RecordTime from UserScoresT
group by UserName) as groupt
on UserScoresT.UserName = groupt.UserName and UserScoresT.RecordTime = groupt.RecordTime;

select UserName,Max(RecordTime) as RecordTime from UserScoresT
group by UserName


select s.UserName, s.PassWord, s.NickName, s.Gender, u.Chinese, u.English, u.Math, u.RecordTime from Table_1 as s
left join
(select tt.* from
(select *, ROW_NUMBER() over (partition by UserName order by RecordTime desc) as rownumber from UserScoresT) as tt
where tt.rownumber = 1) as u
on s.UserName = u.UserName

select tt.* from
(select *, ROW_NUMBER() over (partition by UserName order by RecordTime desc) as rownumber from UserScoresT) as tt
where tt.rownumber = 1;

select * from UserScoresT