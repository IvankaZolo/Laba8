using LAB8.Logic;

var office = new TicketOffice();

var passenger1 = new Passenger(1, "Олена Коваль", "КН123456", "+380991234567");
var passenger2 = new Passenger(2, "Михайло Бойко", "МН654321", "+380507654321");
var passenger3 = new Passenger(3, "Наталія Шевченко", "ШН111222", "+380631112233");

var train1 = new Train(
    id: 1, name: "Експрес Київ-Львів",
    departureStation: "Київ", arrivalStation: "Львів",
    departureTime: new DateTime(2025, 6, 15, 8, 0, 0),
    arrivalTime: new DateTime(2025, 6, 15, 14, 30, 0));

var train2 = new Train(
    id: 2, name: "Нічний Київ-Одеса",
    departureStation: "Київ", arrivalStation: "Одеса",
    departureTime: new DateTime(2025, 6, 15, 22, 0, 0),
    arrivalTime: new DateTime(2025, 6, 16, 6, 0, 0));

var train3 = new Train(
    id: 3, name: "Регіональний Харків-Дніпро",
    departureStation: "Харків", arrivalStation: "Дніпро",
    departureTime: new DateTime(2025, 6, 16, 10, 0, 0),
    arrivalTime: new DateTime(2025, 6, 16, 14, 0, 0));

Console.WriteLine("1. Управління потягами\n");

office.AddTrain(train1);
office.AddTrain(train2);
office.AddTrain(train3);
Console.WriteLine("[1.1] Додано потяги: Київ-Львів, Київ-Одеса, Харків-Дніпро.\n");
var tempTrain = new Train(99, "Тимчасовий Суми-Полтава", "Суми", "Полтава",
    DateTime.Now, DateTime.Now.AddHours(3));
office.AddTrain(tempTrain);
Console.WriteLine($"[1.1] Додано тимчасовий потяг:{tempTrain.Name}");
office.RemoveTrain(99);
Console.WriteLine($"[1.2] Потяг видалено.\n");
Console.WriteLine("[1.3] Всі потяги:");
foreach (var t in office.GetAllTrains())
    Console.WriteLine($"{t.Id}. {t.Name} ({t.DepartureStation} → {t.ArrivalStation})");
Console.WriteLine();
Console.WriteLine("[1.4] Дані потяга #1");
Console.WriteLine(office.GetTrain(1).GetTrainSummary());
Console.WriteLine();

office.AddWagonToTrain(1, new CoupeWagon(number: 1, basePrice: 800m));
office.AddWagonToTrain(1, new CoupeWagon(number: 2, basePrice: 800m));
office.AddWagonToTrain(1, new PlatskarWagon(number: 3, basePrice: 400m));
office.AddWagonToTrain(2, new PlatskarWagon(number: 1, basePrice: 400m));
office.AddWagonToTrain(3, new CoupeWagon(number: 1, basePrice: 750m));
office.AddBooking(passenger1, trainId: 1, wagonNumber: 1, seatNumber: 3);
office.AddBooking(passenger2, trainId: 1, wagonNumber: 1, seatNumber: 7);

Console.WriteLine("[1.5] Вагони потяга #1 з % заброньованих місць");
foreach (var info in office.GetTrainWagonsInfo(trainId: 1))
    Console.WriteLine($"Вагон #{info.Wagon.Number} ({info.Wagon.GetType().Name}): {info.BookingPercentage:F1}% заброньовано. Ціна: {info.Wagon.GetPriceWithMarkup():F2} грн");
Console.WriteLine();


Console.WriteLine("2. Управління вагонами\n");
office.AddWagonToTrain(trainId: 1, new PlatskarWagon(number: 4, basePrice: 380m));
Console.WriteLine("[2.1] Вагон #4 (PlatskarWagon) додано до потяга #1.\n");
Console.WriteLine("[2.2] Спроба видалити вагон #1 (є заброньовані місця)");
try
{
    office.RemoveWagonFromTrain(trainId: 1, wagonNumber: 1);
}
catch (BookingException ex)
{
    Console.WriteLine($"BookingException: {ex.Message}");
}
Console.WriteLine();
Console.WriteLine("[2.2] Видалення вагону #4 (немає бронювань)");
office.RemoveWagonFromTrain(trainId: 1, wagonNumber: 4);
Console.WriteLine("Вагон #4 успішно видалено.\n");
Console.WriteLine("[2.3] Місця вагону #1 потяга #1");
var seats = office.GetWagonSeats(trainId: 1, wagonNumber: 1);
Console.WriteLine($" Вільних: {seats.Available.Count()}");
Console.WriteLine($" Зайнятих: {seats.Booked.Count()} місця: {string.Join(", ", seats.Booked.Select(s => $"#{s.Number}"))}");
Console.WriteLine();


Console.WriteLine("3. Управління бронюваннями\n");
Console.WriteLine("[3.1] Нове бронювання для пасажира Наталії Шевченко на потяг #1, вагон #3, місце #15");
var newBooking = office.AddBooking(passenger3, trainId: 1, wagonNumber: 3, seatNumber: 15);
Console.WriteLine(newBooking.GetInfo());
Console.WriteLine();
Console.WriteLine("[3.4] Перегляд бронювання #1");
Console.WriteLine(office.GetBooking(1).GetInfo());
Console.WriteLine();
Console.WriteLine("[3.3] Зміна бронювання #1 (місце 3 на місце 10)");
office.ChangeBooking(bookingId: 1, newWagonNumber: 1, newSeatNumber: 10);
Console.WriteLine(office.GetBooking(1).GetInfo());
Console.WriteLine();
Console.WriteLine("[3.2] Скасування бронювання #2");
office.CancelBooking(bookingId: 2);
Console.WriteLine("Бронювання #2 скасовано.\n");


Console.WriteLine("4. Пошук\n");
Console.WriteLine("[4.1] Пошук потягів за словом \"Київ\"");
foreach (var t in office.SearchTrains("Київ"))
    Console.WriteLine($"{t.Id}. {t.Name}");
Console.WriteLine();
Console.WriteLine("[4.1] Пошук потягів за словом \"Харків\" ──");
foreach (var t in office.SearchTrains("Харків"))
    Console.WriteLine($"{t.Id}. {t.Name}");
Console.WriteLine();
Console.WriteLine("[4.2] Пошук бронювань за сьогоднішньою датою");
foreach (var b in office.SearchBookingsByDate(DateTime.Today))
    Console.WriteLine($"Бронювання #{b.Id} — {b.Passenger.FullName} — {b.Train.Name}");
Console.WriteLine();