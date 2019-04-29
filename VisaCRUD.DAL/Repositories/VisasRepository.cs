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
                int id = connection.Query<int>(sql, new { country = visa.Country.Id, serviceType = visa.ServiceType.Id, visa.Terms, visa.Validity, visa.Period, visa.Number, visa.WebSite }).Single();

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

        public List<Visa> GetAll()
        {
            string sql = "SELECT t1.*, t2.* FROM Visas t1 "
                + "INNER JOIN Countries t2 ON t2.Id=t1.Country_Id "
                + "INNER JOIN ServiceTypes t3 ON t3.Id=t1.ServiceType_Id "
                + "INNER JOIN VisasDocuments t1t4 ON t1t4.Visa_Id=t1.Id "
                + "INNER JOIN Documents t4 ON t4.Id=t1t4.Document_Id "
                ;

            using (var connection = new SqlConnection(connectionString))
            {
                List<Visa> result = new List<Visa>();

                connection
                    .Query<Visa, Document, Visa>(sql,
                    (_visa, _document) =>
                    {
                        Visa existing = result.FirstOrDefault(x => x.Id == _visa.Id);
                        if (existing == null)
                        {
                            _visa.Documents.Add(_document);
                            result.Add(_visa);
                        }
                        else
                        {
                            existing.Documents.Add(_document);
                        }

                        return _visa;
                    });

                return result;
            }
        }

        public bool Update(Visa visa, int? id)
        {
            if (visa == null)
                throw new ArgumentNullException(nameof(visa));

            String sql = @"
                UPDATE Visas SET Country_Id = @country, ServiceType_Id = @serviceType, Terms = @terms, Validity = @validity, Period = @period, Number = @number, WebSite = @website) WHERE Id = @id;
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = new SqlConnection(connectionString))
            {
                int newId = connection.Query<int>(sql, new { country = visa.Country.Id, serviceType = visa.ServiceType.Id, visa.Terms, visa.Validity, visa.Period, visa.Number, visa.WebSite }).Single();

                if (newId != visa.Id)
                    return false;

                sql = @"DELETE FROM VisasDocuments WHERE Visa_Id = @id";

                connection.Execute(sql, new { visa.Id });

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
    }
}
