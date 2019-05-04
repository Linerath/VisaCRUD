using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VisaCRUD.DAL.Entities;
using VisaCRUD.DAL.Interfaces;

namespace VisaCRUD.DAL.Repositories
{
    public class VisasRepository : IVisasRepository
    {
        private String connectionString;

        public VisasRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(Visa visa)
        {
            if (visa == null)
                throw new ArgumentNullException(nameof(visa));

            String sql = @"
                INSERT INTO Visas (Country_Id, ServiceType_Id, Terms, Validity, Period, Number, WebSite) Values (@Country, @ServiceType, @Terms, @Validity, @Period, @Number, @WebSite);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = new SqlConnection(connectionString))
            {
                int id = connection.Query<int>(sql, new { country = visa.Country.Id, serviceType = visa.ServiceType?.Id, visa.Terms, visa.Validity, visa.Period, visa.Number, visa.WebSite }).Single();

                if (visa.Documents?.Count > 0)
                {
                    sql = "INSERT INTO VisasDocuments (Visa_Id, Document_Id) Values (@visaId, @documentId)";

                    var list = visa.Documents.Select(x => new
                    {
                        visaId = id,
                        documentId = x.Id,
                    });

                    connection.Execute(sql, list);
                }

                return id;
            }
        }

        public bool Add(List<Visa> visas)
        {
            if (visas == null)
                throw new ArgumentNullException(nameof(visas));

            foreach (var item in visas)
            {
                if (Add(item) == -1)
                    return false;
            }

            return true;
        }

        public void Delete(Visa visa)
        {
            if (visa == null)
                throw new ArgumentNullException(nameof(visa));

            String sql = @"DELETE FROM VisasDocuments WHERE Visa_Id = @id";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, new { visa.Id });

                sql = @"DELETE FROM Visas WHERE Id = @id";

                connection.Execute(sql, new { visa.Id });
            }
        }

        public void Delete(int id)
        {
            String sql = @"DELETE FROM VisasDocuments WHERE Visa_Id = @id";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, new { id });

                sql = @"DELETE FROM Visas WHERE Id = @id";

                connection.Execute(sql, new { id });
            }
        }

        public List<Visa> GetAll(int? countryId = null)
        {
            string sql = "SELECT t1.*, t2.*, t3.*, t4.* FROM Visas t1 "
                + "INNER JOIN Countries t2 ON t2.Id=t1.Country_Id "
                + "LEFT JOIN ServiceTypes t3 ON t3.Id=t1.ServiceType_Id "
                + "LEFT JOIN VisasDocuments t1t4 ON t1t4.Visa_Id=t1.Id "
                + "LEFT JOIN Documents t4 ON t4.Id=t1t4.Document_Id "
                ;

            if (countryId.HasValue)
                sql += "WHERE t2.Id=@id";

            using (var connection = new SqlConnection(connectionString))
            {
                List<Visa> result = new List<Visa>();

                connection
                    .Query<Visa, Country, ServiceType, Document, Visa>(sql,
                    (_visa, _country, _serviceType, _document) =>
                    {

                        Visa existing = result.FirstOrDefault(x => x.Id == _visa.Id);
                        if (existing == null)
                        {
                            _visa.Country = _country;
                            if (_serviceType != null)
                                _visa.ServiceType = _serviceType;
                            if (_document != null)
                                _visa.Documents.Add(_document);
                            result.Add(_visa);
                        }
                        else
                        {
                            if (_document != null)
                                existing.Documents.Add(_document);
                        }

                        return _visa;
                    },
                    new { id = countryId });

                return result;
            }
        }

        public List<Country> GetAllCountries()
        {
            string sql = "SELECT * FROM Countries";

            using (var connection = new SqlConnection(connectionString))
            {
                List<Country> result = connection.Query<Country>(sql).ToList();

                return result;
            }
        }

        public bool Update(Visa visa, int? id = null)
        {
            if (visa == null)
                throw new ArgumentNullException(nameof(visa));

            String sql = @"
                UPDATE Visas SET Country_Id = @country, ServiceType_Id = @serviceType, Terms = @terms, Validity = @validity, Period = @period, Number = @number, WebSite = @website WHERE Id = @id";

            if (id == null)
                id = visa.Id;

            using (var connection = new SqlConnection(connectionString))
            {
                int result = connection.Execute(sql, new { country = visa.Country.Id, serviceType = visa.ServiceType?.Id, visa.Terms, visa.Validity, visa.Period, visa.Number, visa.WebSite, id });

                if (result != 1)
                    return false;

                sql = @"DELETE FROM VisasDocuments WHERE Visa_Id = @id";

                connection.Execute(sql, new { id });

                sql = "INSERT INTO VisasDocuments (Visa_Id, Document_Id) Values (@visaId, @documentId)";

                if (visa.Documents?.Count > 0)
                {
                    var list = visa.Documents.Select(x => new
                    {
                        visaId = id,
                        documentId = x.Id,
                    });

                    connection.Execute(sql, list);
                }

                return true;
            }
        }

        public Country GetCountryById(int id)
        {
            string sql = "SELECT * FROM Countries WHERE Id=@id";

            using (var connection = new SqlConnection(connectionString))
            {
                Country result = connection.QueryFirst<Country>(sql, new { id });

                return result;
            }
        }

        public List<ServiceType> GetAllServiceTypes()
        {
            string sql = "SELECT * FROM ServiceTypes";

            using (var connection = new SqlConnection(connectionString))
            {
                List<ServiceType> result = connection.Query<ServiceType>(sql).ToList();

                return result;
            }
        }

        public List<Document> GetAllDocuments()
        {
            string sql = "SELECT * FROM Documents";

            using (var connection = new SqlConnection(connectionString))
            {
                List<Document> result = connection.Query<Document>(sql).ToList();

                return result;
            }
        }

        public Visa GetVisaById(int visaId)
        {
            string sql = "SELECT t1.*, t2.*, t3.*, t4.* FROM Visas t1 "
                + "INNER JOIN Countries t2 ON t2.Id=t1.Country_Id "
                + "LEFT JOIN ServiceTypes t3 ON t3.Id=t1.ServiceType_Id "
                + "LEFT JOIN VisasDocuments t1t4 ON t1t4.Visa_Id=t1.Id "
                + "LEFT JOIN Documents t4 ON t4.Id=t1t4.Document_Id "
                + "WHERE t1.Id=@id"
                ;

            using (var connection = new SqlConnection(connectionString))
            {
                Visa result = null;

                connection
                    .Query<Visa, Country, ServiceType, Document, Visa>(sql,
                    (_visa, _country, _serviceType, _document) =>
                    {
                        if (result == null || result.Id == _visa?.Id)
                            result = _visa;

                        if (_visa.Country == null && _country != null)
                            _visa.Country = _country;
                        if (_visa.ServiceType == null && _serviceType != null)
                            _visa.ServiceType = _serviceType;
                        if (_document != null)
                            _visa.Documents.Add(_document);

                        return _visa;
                    },
                    new { id = visaId });

                return result;
            }
        }
    }
}
