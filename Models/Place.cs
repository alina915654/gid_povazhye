namespace WebApplication1.Models
{
    // Это класс, описывающий одно место (кафе, отель или музей)
    public class Place
    {
        public int Id { get; set; } // Уникальный номер
        public string Name { get; set; } // Название (например, "Отель Юрьево")
        public string? Description { get; set; } // Описание
        public string? ImageUrl { get; set; } // Ссылка на картинку

        // Тип места: "Еда", "Ночлег", "Достопримечательность"
        public string Type { get; set; }

        // Город
        public string City { get; set; }

        public string Address { get; set; } // Адрес

        public string? WorkingHours { get; set; } //Время работы
        public string? MapLink { get; set; } // Ссылка на карты (Яндекс/Google)
                                             // Координаты для карты (Тип double - это дробное число)
        public double Latitude { get; set; }  // Широта
        public double Longitude { get; set; } // Долгота
        public string? ContactName { get; set; }  
        public string? ContactPhone { get; set; }

        public bool HasWifi { get; set; }        // Есть Wi-Fi?
        public bool HasParking { get; set; }     // Есть Парковка?
        public bool HasPayment { get; set; }     // Оплата картой?
        public bool HasGuide { get; set; }       // Есть Гид/Экскурсия?
        public bool IsEco { get; set; } // Галочка "Это эко-объект"
    }
}
