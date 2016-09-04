using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using Zhigly.Code.Objects;

namespace Zhigly.Code
{
    public class Database
    {
        private static readonly MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings[Constants.Database].ConnectionString);

        public static int RegisterUser(College school, string email, string primaryname, string secondaryname, string password, bool organization, int verification)
        {
            MySqlCommand command = null;

            try
            {
                connection.Open();

                string query = "INSERT into users (email, password, school, primaryname, secondaryname, last_ip, organization, status) VALUES (@Email, @Password, @School, @PrimaryName, @SecondaryName, @LastIp, @Organization, @Verification); select last_insert_id();";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", Hashing.Password(password));
                command.Parameters.AddWithValue("@School", school.Id);
                command.Parameters.AddWithValue("@PrimaryName", primaryname);
                command.Parameters.AddWithValue("@SecondaryName", secondaryname);
                command.Parameters.AddWithValue("@LastIp", Utility.GetIpAddress());
                command.Parameters.AddWithValue("@Organization", organization ? 1 : 0);
                command.Parameters.AddWithValue("@Verification", verification);

                int userId = Convert.ToInt32(command.ExecuteScalar());

                command.Dispose();
                connection.Close();

                return userId;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return -1;
        }

        public static void SetSchoolStats()
        {
            MySqlDataReader reader = null;
            MySqlCommand command = null;

            try
            {
                connection.Open();

                string query = "SELECT school, COUNT(*) as info FROM users GROUP BY school ASC UNION SELECT school, COUNT(*) as listings FROM listings GROUP BY school ORDER BY school ASC";

                command = new MySqlCommand(query, connection);

                reader = command.ExecuteReader();

                command.Dispose();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt16("school");
                        int users = reader.GetInt16("info");

                        reader.Read();

                        int listings = reader.GetInt16("info");

                        College college = College.Get(id);

                        if (college == null)
                        {
                            continue;
                        }

                        college.Users = users;
                        college.Listings = listings;

                    }

                    reader.Close();
                    reader.Dispose();
                    command.Dispose();
                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public static object GetData(MySqlDataReader reader, string col)
        {
            var data = reader[col];
            return data != DBNull.Value ? data : null;
        }

        public static string GetStringData(MySqlDataReader reader, string col)
        {
            var data = GetData(reader, col);
            return data != null ? data.ToString() : "";
        }


        public static bool UpdateUserInfo(string email, string primaryname, string secondaryname, string phone)
        {
            MySqlCommand command = null;

            try
            {
                connection.Open();

                string query = "UPDATE users SET primaryname = @PrimaryName, secondaryname = @SecondaryName, phone = @Phone WHERE email = @Email";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@PrimaryName", primaryname);
                command.Parameters.AddWithValue("@SecondaryName", secondaryname);
                command.Parameters.AddWithValue("@Phone", phone);

                command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                return true;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }

                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return false;
        }

        public static bool VerifyEmail(int id)
        {
            MySqlCommand command = null;

            try
            {
                connection.Open();

                string query = "UPDATE users SET status = 1 WHERE id = @Id";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                return true;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return false;
        }

        public static bool UpdateUserPassword(string email, string password)
        {
            MySqlCommand command = null;

            try
            {
                connection.Open();

                string query = "UPDATE users SET password = @Password WHERE email = @Email";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Password", Hashing.Password(password));
                command.Parameters.AddWithValue("@Email", email);

                command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                return true;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return false;
        }

        public static bool UpdateUser(string email, string primaryname, string secondaryname, string phone, string password)
        {
            MySqlCommand command = null;

            try
            {
                connection.Open();

                string query = "UPDATE users SET primaryname = @PrimaryName, secondaryname = @SecondaryName, phone = @Phone, password = @Password WHERE email = @Email";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", Hashing.Password(password));
                command.Parameters.AddWithValue("@PrimaryName", primaryname);
                command.Parameters.AddWithValue("@SecondaryName", secondaryname);
                command.Parameters.AddWithValue("@Phone", phone);

                command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                return true;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return false;
        }

        public static bool UpdateLastLogin(string email)
        {
            MySqlCommand command = null;

            try
            {
                connection.Open();

                string query = "UPDATE users SET last_login = @Timestamp WHERE email = @Email";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@Email", email);

                command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                return true;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return false;
        }

        public static bool AddListingView(int id)
        {
            MySqlCommand command = null;

            try
            {
                connection.Open();

                string query = "UPDATE listings SET views = views + 1 WHERE id = @Id";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                return true;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return false;
        }

        public static bool AddPostView(int id)
        {
            MySqlCommand command = null;

            try
            {
                connection.Open();

                string query = "UPDATE posts SET views = views + 1 WHERE id = @Id";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                return true;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return false;
        }

        public static User GetUser(int id)
        {
            return GetUser(id, null);
        }

        public static User GetUser(string email)
        {
            return GetUser(-1, email);
        }

        public static User GetUser(int userid, string userEmail)
        {
            MySqlDataReader reader = null;
            MySqlCommand command = null;

            string column = userid > 0 ? "id" : "email";
            string value = userid > 0 ? userid.ToString() : userEmail;

            try
            {
                connection.Open();

                string query = "SELECT id, email, password, school, primaryname, secondaryname, phone, credit, organization, status FROM users WHERE " + column + " = @Value";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Value", value);

                reader = command.ExecuteReader();

                command.Dispose();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt16("id");
                        string email = reader.GetString("email");
                        string password = reader.GetString("password");
                        int school = reader.GetInt16("school");
                        string primaryname = GetStringData(reader, "primaryname");
                        string secondaryname = GetStringData(reader, "secondaryname");
                        string phone = GetStringData(reader, "phone");
                        int credit = reader.GetInt32("credit");
                        bool organization = reader.GetInt16("organization") == 1;
                        int status = reader.GetInt32("status");

                        User user = new User
                        {
                            Id = id,
                            Email = email,
                            Password = password,
                            School = College.Get(school),
                            PrimaryName = primaryname,
                            SecondaryName = secondaryname,
                            PhoneNumber = phone,
                            Credit = credit,
                            Organization = organization,
                            Status = status
                        };

                        reader.Close();
                        reader.Dispose();
                        command.Dispose();
                        connection.Close();

                        return user;
                    }
                }
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return null;
        }

        public static string CheckDb(string email, string password)
        {
            MySqlCommand command = null;

            try
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM users WHERE email = @Email AND password = @Password";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", Hashing.Password(password));

                bool ret = Convert.ToInt32(command.ExecuteScalar()) == 1;

                command.Dispose();
                connection.Close();

                return "Good Connection";
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        public static bool AuthenticateUser(string email, string password)
        {
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            try
            {
                connection.Open();

                string query = "SELECT password FROM users WHERE email = @Email";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Email", email);

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string hash = GetStringData(reader, "password");

                        reader.Close();
                        reader.Dispose();
                        command.Dispose();
                        connection.Close();

                        return Hashing.Validate(password, hash);
                    }
                }
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return false;
        }

        public static string IsEmailFree(string email)
        {
            MySqlCommand command = null;
            try
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM users WHERE email = @Email";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Email", email);

                int count = Convert.ToInt32(command.ExecuteScalar());

                command.Dispose();
                connection.Close();

                return count == 0 ? null : "This email address is already in use.";
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return "This email address is already in use.";
        }

        public static int PostAd(User user, string title, int category, int subcategory, string description, List<String> images, DateTime expiration, bool anonymous, bool boosted)
        {
            MySqlCommand command = null;
            try
            {
                connection.Open();

                string query = "INSERT into listings (school, category, subcategory, user, title, description, expiration, anonymous, boosted) VALUES (@School, @Category, @Subcategory, @User, @Title, @Description, @Expiration, @Anonymous, @Boosted); select last_insert_id();";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@School", user.School);
                command.Parameters.AddWithValue("@Category", category);
                command.Parameters.AddWithValue("@Subcategory", subcategory);
                command.Parameters.AddWithValue("@User", user.Id);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Expiration", expiration);
                command.Parameters.AddWithValue("@Anonymous", anonymous ? 1 : 0);
                command.Parameters.AddWithValue("@Boosted", boosted ? 1 : 0);
                 int listingId = Convert.ToInt32(command.ExecuteScalar());

                if (images.Count > 0)
                {

                    query = "INSERT into images (listing, image, deleted) VALUES";

                    foreach (string image in images)
                    {
                        query += " (" + listingId + ", '" + image + "', 0),";
                    }

                    query = query.TrimEnd(',') + ";";

                    command = new MySqlCommand(query, connection);

                    command.ExecuteNonQuery();
                }

                command.Dispose();
                connection.Close();

                return listingId;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return -1;
        }

        public static bool MakePurchase(User user, int listing, bool anonymous, bool boosted, int days, int cost)
        {
            MySqlCommand command = null;

            try
            {
                connection.Open();

                string query = "UPDATE users SET credit = credit - @Cost WHERE id = @Id";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Cost", cost);
                command.Parameters.AddWithValue("@Id", user.Id);

                command.ExecuteNonQuery();
                
                query = "INSERT into purchases (user, listing, anonymous, boosted, days, cost) VALUES (@User, @Listing, @Anonymous, @Boosted, @Days, @Cost)";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@User", user.Id);
                command.Parameters.AddWithValue("@Listing", listing);
                command.Parameters.AddWithValue("@Anonymous", anonymous ? 1 : 0);
                command.Parameters.AddWithValue("@Boosted", boosted ? 1 : 0);
                command.Parameters.AddWithValue("@Days", days);
                command.Parameters.AddWithValue("@Cost", cost);
                
                command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                return true;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return false;
        }

        public static bool DeleteAd(int id)
        {
            MySqlCommand command = null;

            try
            {
                connection.Open();

                string query = "UPDATE listings SET deleted = 1 WHERE id = @Id";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                return true;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return false;
        }

        public static bool UpdateAd(int id, string title, int category, int subcategory, string description, List<string> added, List<string> deleted)
        {
            foreach (string image in deleted)
            {
                Debug.WriteLine("Deleted: " + image);
            }
            foreach (string image in added)
            {
                Debug.WriteLine("Added: " + image);
            }

            MySqlCommand command = null;

            try
            {
                connection.Open();

                string query = "UPDATE listings SET category = @Category, subcategory = @Subcategory, title = @Title, description = @Description, edited = @Edited WHERE id = @Id";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Category", category);
                command.Parameters.AddWithValue("@Subcategory", subcategory);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Edited", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();

                if (deleted.Count > 0)
                {

                    query = "UPDATE images SET deleted = 1 WHERE listing = @Id AND image IN (";

                    foreach (string image in deleted)
                    {
                        query += "'" + image + "',";
                    }

                    query = query.TrimEnd(',') + ");";

                    command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }

                if (added.Count > 0)
                {
                    query = "INSERT into images (listing, image, deleted) VALUES";

                    foreach (string image in added)
                    {
                        query += " (" + id + ", '" + image + "', 0),";
                    }

                    query = query.TrimEnd(',') + ";";


                    command = new MySqlCommand(query, connection);

                    command.ExecuteNonQuery();
                }

                command.Dispose();
                connection.Close();

                return true;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return false;
        }
        
        public static Advertisement GetAd(int adid)
        {
            MySqlCommand command = null;
            MySqlDataReader reader = null;

            try
            {
                connection.Open();

                string query = "SELECT school, category, subcategory, user, title, description, views, created, boosted, anonymous, expiration, listings.deleted, group_concat(images.image separator ',') as 'images' FROM listings LEFT JOIN images on listings.id = images.listing WHERE listings.id = @Id AND images.deleted = 0";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", adid);

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        int school = reader.GetInt16("school");
                        int category = reader.GetInt16("category");
                        int subcategory = reader.GetInt16("subcategory");
                        int user = reader.GetInt16("user");
                        string title = GetStringData(reader, "title");
                        string images = GetStringData(reader, "images");
                        string description = GetStringData(reader, "description");
                        int views = reader.GetInt16("views");
                        bool boosted = reader.GetInt16("boosted") == 1;
                        bool deleted = reader.GetInt16("deleted") == 1;
                        bool anonymous = reader.GetInt16("anonymous") == 1;
                        DateTime expiration = DateTime.Parse(GetStringData(reader, "expiration"));
                        DateTime created = reader.GetDateTime("created");

                        Advertisement ad = new Advertisement();
                        ad.Id = adid;
                        ad.School = school;
                        ad.Category = category;
                        ad.Subcategory = subcategory;
                        ad.User = user;
                        ad.Title = title;
                        ad.Description = description;
                        ad.AddImages(images);
                        ad.Views = views;
                        ad.Created = created;
                        ad.Anonymous = anonymous;
                        ad.Boosted = boosted;
                        ad.Expiration = expiration;
                        ad.Deleted = deleted;

                        reader.Dispose();
                        reader.Close();
                        command.Dispose();
                        connection.Close();

                        return ad;
                    }
                }
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return null;
        }

        public static List<Advertisement> GetAds(int schoolid, string and, int page, string order)
        {
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            List<Advertisement> ads = new List<Advertisement>();

            try
            {
                connection.Open();

                string query = "SELECT listings.id, school, category, subcategory, user, title, description, views, created, boosted, anonymous, group_concat(if (images.deleted = 0, images.image, null) separator ',') as `images` FROM listings LEFT JOIN images on listings.id = images.listing WHERE school = @SchoolId " + and + " AND expiration > NOW() AND listings.deleted = 0 group by id ORDER BY " + order + "created DESC LIMIT @Page, @PerPage";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@SchoolId", schoolid);
                command.Parameters.AddWithValue("@Page", (page - 1) * Constants.ListingsPerPage);
                command.Parameters.AddWithValue("@PerPage", Constants.ListingsPerPage);

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt16("id");
                        int category = reader.GetInt16("category");
                        int subcategory = reader.GetInt16("subcategory");
                        int user = reader.GetInt16("user");
                        string title = GetStringData(reader, "title");
                        string images = GetStringData(reader, "images");
                        string description = GetStringData(reader, "description");
                        int views = reader.GetInt16("views");
                        DateTime created = reader.GetDateTime("created");
                        bool boosted = reader.GetInt16("boosted") == 1;
                        bool anonymous = reader.GetInt16("anonymous") == 1;

                        Advertisement ad = new Advertisement();
                        ad.Id = id;
                        ad.School = schoolid;
                        ad.Category = category;
                        ad.Subcategory = subcategory;
                        ad.User = user;
                        ad.Title = title;
                        ad.Description = description;
                        ad.AddImages(images);
                        ad.Views = views;
                        ad.Anonymous = anonymous;
                        ad.Created = created;
                        ad.Boosted = boosted;

                        ads.Add(ad);
                    }
                }

                command.Dispose();
                connection.Close();

                return ads;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return ads;
        }

        public static bool Log(string message)
        {
            MySqlCommand command = null;

            if (true) { return true; }

            try
            {
                connection.Open();

                string query = "INSERT into log (message) VALUES (@Message)";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Message", message);

                command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                return true;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return false;
        }

        public static bool Report(int listing, int reason)
        {
            MySqlCommand command = null;
            
            try
            {
                connection.Open();

                string query = "INSERT into reports (listing, reason) VALUES (@Listing, @Reason)";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Listing", listing);
                command.Parameters.AddWithValue("@Reason", reason);

                command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                return true;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return false;
        }

        public static BlogPost GetBlogPost(int number)
        {
            MySqlCommand command = null;
            MySqlDataReader reader = null;

            try
            {
                connection.Open();

                string query = "SELECT id, user, title, content, image, views, created FROM posts WHERE id = @Id ORDER BY created DESC";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", number);

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        int id = reader.GetInt16("id");
                        string user = GetStringData(reader, "user");
                        string title = GetStringData(reader, "title");
                        string content = GetStringData(reader, "content");
                        string image = GetStringData(reader, "image");
                        int views = reader.GetInt16("views");
                        DateTime created = reader.GetDateTime("created");

                        BlogPost post = new BlogPost
                        {
                            Id = id,
                            User = user,
                            Title = title,
                            Content = content,
                            Image = image,
                            Views = views,
                            Created = created
                        };

                        reader.Dispose();
                        reader.Close();
                        command.Dispose();
                        connection.Close();

                        return post;
                    }
                }


                return null;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return null;
        }

        public static List<BlogPost> GetBlogPosts()
        {
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            List<BlogPost> posts = new List<BlogPost>();

            try
            {
                connection.Open();

                string query = "SELECT id, user, title, content, image, views, created FROM posts ORDER BY created DESC";

                command = new MySqlCommand(query, connection);

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        int id = reader.GetInt16("id");
                        string user = GetStringData(reader, "user");
                        string title = GetStringData(reader, "title");
                        string content = GetStringData(reader, "content");
                        string image = GetStringData(reader, "image");
                        int views = reader.GetInt16("views");
                        DateTime created = reader.GetDateTime("created");

                        BlogPost post = new BlogPost
                        {
                            Id = id,
                            User = user,
                            Title = title,
                            Content = content,
                            Image = image,
                            Views = views,
                            Created = created
                        };

                        posts.Add(post);
                    }
                }

                reader.Dispose();
                reader.Close();
                command.Dispose();
                connection.Close();

                return posts;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return posts;
        }

        public static List<Advertisement> GetUserAds(int user)
        {
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            List<Advertisement> ads = new List<Advertisement>();

            try
            {
                connection.Open();

                string query = "SELECT id, category, subcategory, title, description, boosted, anonymous, views, created FROM listings WHERE user = @User AND deleted = 0 ORDER BY created DESC";

                command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@User", user);

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt16("id");
                        int category = reader.GetInt16("category");
                        int subcategory = reader.GetInt16("subcategory");
                        string title = GetStringData(reader, "title");
                        string description = GetStringData(reader, "description");
                        bool boosted = reader.GetInt16("boosted") == 1;
                        bool anonymous = reader.GetInt16("anonymous") == 1;
                        int views = reader.GetInt16("views");
                        DateTime created = reader.GetDateTime("created");

                        Advertisement ad = new Advertisement();
                        ad.Id = id;
                        ad.Category = category;
                        ad.Subcategory = subcategory;
                        ad.Title = title;
                        ad.Description = description;
                        ad.Boosted = boosted;
                        ad.Anonymous = anonymous;
                        ad.Views = views;
                        ad.Created = created;

                        ads.Add(ad);
                    }
                }

                reader.Dispose();
                reader.Close();
                command.Dispose();
                connection.Close();

                return ads;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return ads;
        }
    }
}