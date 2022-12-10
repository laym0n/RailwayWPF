
select st.Name from Train t
join StationTrainSchedule sts on sts.IdTrain = t.Id
join Station st on st.Id = sts.IdStation
group by t.Id, sts.NumberInTrip, st.Name