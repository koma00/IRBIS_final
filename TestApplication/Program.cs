using System;
using System.Linq;

using ManagedClient;
using WpfTestApplication;
using System.Collections.Generic;

namespace TestApplication
{
    class Program
    {
        private static string domain;

        private static void RDR(ManagedClient64 client)
        {
            // Делаем переключение на базу RDR
            client.PushDatabase("RDR");

            Console.WriteLine("PushDatabase RDR");

            /*
             * Разбитие массива данных на чанки, и отправка в БД
             */

            int maxMfn = client.GetMaxMfn() - 1;
            int chunkLength = 25;
            int mfn = 0;
            Console.WriteLine("Total records: " + maxMfn);

            for (int loop = 0; loop < (int)Math.Ceiling((double)maxMfn / chunkLength); loop++)
            {
                /*
                 * Подготовка данных перед записью
                 */

                string signupZombie = "";
                for (int i = 0; i < chunkLength; i++)
                {
                    mfn++;
                    if (mfn <= maxMfn)
                    {
                        IrbisRecord record = client.ReadRecord(mfn);
                        if (record != null)
                        {
                            // Формируем чанк данных для переноса
                            signupZombie +=
                                "user[" + i + "][name]=" + record.FM("11") + "&" +
                                "user[" + i + "][surname]=" + record.FM("10") + "&" +
                                "user[" + i + "][login]=" + record.FM("30") + "&";
                        }
                    }
                }

                /*
                 * Методы отправки данных в БД
                 */
                MyWebRequest signupZombiee = new MyWebRequest(domain + "user/" + "signupZombie", "POST", signupZombie);
                Console.WriteLine("signupZombie: " + signupZombiee.GetResponse());
                Console.WriteLine(new string('-', 60));
            }

        }

        static string Title_BOOKS(IrbisRecord record)
        {
            string title = null;
            bool flag = true;

            title = record.FM("200", 'A') + " [Электронный ресурс] " + record.FM("200", 'E') + " " + record.FM("923", 'H') + " " + record.FM("923", 'I') + " / ";
            if (record.FM("700") != null)
            {
                flag = false;
                title += record.FM("700", 'B') + " " + record.FM("700", 'A');
            }
            RecordField[] author_records = record.Fields.GetField("701").ToArray();
            foreach (var author_record in author_records)
            {
                if (flag)
                {
                    title += author_record.GetSubFieldText('B', 0) + " " + author_record.GetSubFieldText('A', 0);
                    flag = false;
                }
                else
                {
                    title += ", " + author_record.GetSubFieldText('B', 0) + " " + author_record.GetSubFieldText('A', 0);
                }
            }
            title += ". - " + record.FM("230", 'A') + ". - " + record.FM("210", 'A') + " : " + record.FM("210", 'C') + ", " + record.FM("210", 'D');
            title += ". - " + record.FM("215", 'A') + "с. : ";
            if (record.FM("215", 'C') != null)
            {
                title += record.FM("215", 'C').Remove(0, 2);
            }
            if (record.FM("215", '0') != null)
            {
                title += record.FM("215", '0').Remove(0, 2);
            }
            title += " - (" + record.FM("225", 'A') + ")" + record.FM("300");

            return title;
        }
        static string Title_BOOKS(IrbisRecord record, IrbisRecord record2)
        {
            string title = null;

            if (record2.FM("200", 'A') != null)
            {
                title += record2.FM("200", 'A');
            }

            if (record2.FM("200", 'E') != null)
            {
                title += " : " + record2.FM("200", 'E');
            }

            if (record2.FM("210", 'A') != null)
            {
                title += ". - " + record2.FM("210", 'A');
            }

            if (record2.FM("210", 'C') != null)
            {
                title += " : " + record2.FM("210", 'C');
            }

            if (record2.FM("210", 'D') != null)
            {
                title += ". - " + record2.FM("210", 'D');
            }

            if (record2.FM("110", 'D') != null)
            {
                title += ". - Выходит ";
                switch (record2.FM("110", 'D'))
                {
                    case "a":
                        title += "ежедневно";
                        break;
                    case "b":
                        title += "дважды в неделю";
                        break;
                    case "c":
                        title += "еженедельно";
                        break;
                    case "d":
                        title += "раз в две недели";
                        break;
                    case "e":
                        title += "дважды в месяц";
                        break;
                    case "f":
                        title += "ежемесячно";
                        break;
                    case "10":
                        title += "10 в год";
                        break;
                    case "8":
                        title += "8 в год";
                        break;
                    case "7":
                        title += "7 в год";
                        break;
                    case "g":
                        title += "раз в два месяца";
                        break;
                    case "h":
                        title += "ежеквартально";
                        break;
                    case "i":
                        title += "три раза в год";
                        break;
                    case "j":
                        title += "дважды в год";
                        break;
                    case "k":
                        title += "ежегодно";
                        break;
                    case "l":
                        title += "раз в два года";
                        break;
                    case "m":
                        title += "раз в три года";
                        break;
                    case "n":
                        title += "три раза в неделю";
                        break;
                    case "208":
                        title += "четыре раза в неделю";
                        break;
                    case "o":
                        title += "три раза в месяц";
                        break;
                    case "u":
                        title += "неизвестно";
                        break;
                    case "y":
                        title += "нерегулярно";
                        break;
                    case "z":
                        title += "другая";
                        break;
                }
            }

            if (record.FM("934") != null)
            {
                title += " " + record.FM("934") + "г.";
            }

            if (record.FM("931", '9') != null)
            {
                title += " " + record.FM("931", '9') + " ";
            }

            if (record.FM("936") != null)
            {
                title += " " + record.FM("936");
            }

            return title;
        }

        private static void BOOKS(ManagedClient64 client)
        {
            // Делаем переключение на базу BOOKS
            client.PushDatabase("BOOKS");

            Console.WriteLine("PushDatabase BOOKS");

            /*
             * Разбитие массива данных на чанки, и отправка в БД
             */

            int maxMfn = client.GetMaxMfn() - 1;
            int chunkLength = 25;
            int mfn = 0;
            Console.WriteLine("Total records: " + maxMfn);

            for (int loop = 0; loop < (int)Math.Ceiling((double)maxMfn / chunkLength); loop++)
            {
                /*
                 * Подготовка данных перед записью
                 */

                string books = "";
                string authors = "";
                string bookAuthor = "";
                string keyword = "";
                string bookKeyword = "";
                string bookDelete = "";
                int j = 0;
                int k = 0;
                int l = 0;
                for (int i = 0; i < chunkLength; i++)
                {
                    mfn++;
                    if (mfn <= maxMfn)
                    {
                        IrbisRecord record = client.ReadRecord(mfn);

                        //Удаляем с бд книги с отсутствующими mfn
                        if (record == null || record.FM("903").Length == 3 || record.FM("903").Length == 7)
                        {
                            bookDelete +=
                                "book[" + l + "][category_id]=" + "2" + "&" +
                                "book[" + l + "][mfn]=" + mfn + "&";
                            l++;
                        }
                        else
                        {
                            if (record.FM("933") != null)
                            {
                                bool fl = true;
                                int mfn2 = 0;
                                IrbisRecord record2 = null;
                                while (fl && (mfn2 <= maxMfn))
                                {
                                    mfn2++;
                                    record2 = client.ReadRecord(mfn2);
                                    if (record.FM("933") == record2.FM("903"))
                                    {
                                        fl = false;
                                    }
                                }
                                // Формируем чанк данных для переноса
                                books +=
                                    "book[" + i + "][category_id]=" + "2" + "&" +
                                    "book[" + i + "][mfn]=" + record.Mfn + "&" +
                                    "book[" + i + "][name]=" + Title_BOOKS(record, record2) + "&" +
                                    "book[" + i + "][year]=" + record.FM("934") + "&" +
                                    "book[" + i + "][udk]=" + record.FM("675") + "&" +
                                    "book[" + i + "][link]=" + "/BOOKS" + record.FM("951", 'a') + "&" +
                                    "book[" + i + "][link_name]=" + record.FM("951", 't') + "&";
                            }

                            if (record.FM("933") == null)
                            {
                                // Формируем чанк данных для переноса
                                books +=
                                    "book[" + i + "][category_id]=" + "2" + "&" +
                                    "book[" + i + "][mfn]=" + record.Mfn + "&" +
                                    "book[" + i + "][name]=" + Title_BOOKS(record) + "&" +
                                    "book[" + i + "][year]=" + record.FM("210", 'd') + "&" +
                                    "book[" + i + "][udk]=" + record.FM("675") + "&" +
                                    "book[" + i + "][link]=" + "/BOOKS" + record.FM("951", 'a') + "&" +
                                    "book[" + i + "][link_name]=" + record.FM("951", 't') + "&";
                            }

                            if (record.FM("700", 'a') != null)
                            {
                                authors +=
                                    "author[" + j + "][surname]=" + record.FM("700", 'a') + "&" +
                                    "author[" + j + "][initials]=" + record.FM("700", 'b') + "&";
                                bookAuthor +=
                                    "attach[" + j + "][category_id]=" + "2" + "&" +
                                    "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + j + "][surname]=" + record.FM("700", 'a') + "&" +
                                    "attach[" + j + "][initials]=" + record.FM("700", 'b') + "&";
                                j++;
                            }
                            RecordField[] author_records = record.Fields.GetField("701").ToArray();
                            foreach (var author_record in author_records)
                            {
                                if (!((record.FM("700", 'a') == author_record.GetSubFieldText('a', 0)) &&
                                    (record.FM("700", 'b') == author_record.GetSubFieldText('b', 0))))
                                {
                                    authors +=
                                        "author[" + j + "][surname]=" + author_record.GetSubFieldText('a', 0) + "&" +
                                        "author[" + j + "][initials]=" + author_record.GetSubFieldText('b', 0) + "&";
                                    bookAuthor +=
                                        "attach[" + j + "][category_id]=" + "2" + "&" +
                                        "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                        "attach[" + j + "][surname]=" + author_record.GetSubFieldText('a', 0) + "&" +
                                        "attach[" + j + "][initials]=" + author_record.GetSubFieldText('b', 0) + "&";
                                    j++;
                                }
                            }
                            RecordField[] editor_records = record.Fields.GetField("702").ToArray();
                            foreach (var editor_record in editor_records)
                            {
                                authors +=
                                    "author[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "author[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                bookAuthor +=
                                    "attach[" + j + "][category_id]=" + "2" + "&" +
                                    "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "attach[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                j++;
                            }
                            RecordField[] keyword_records = record.Fields.GetField("610").ToArray();
                            foreach (var keyword_record in keyword_records)
                            {
                                keyword +=
                                    "keyword[" + k + "][value]=" + keyword_record.GetFieldText() + "&";
                                bookKeyword +=
                                    "attach[" + k + "][category_id]=" + "2" + "&" +
                                    "attach[" + k + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + k + "][value]=" + keyword_record.GetFieldText() + "&";
                                k++;
                            }
                        }
                    }
                }

                /*
                 * Методы отправки данных в БД
                 */
                MyWebRequest deleteBook = new MyWebRequest(domain + "catalog/book/delete", "POST", bookDelete);
                Console.WriteLine("deleteBook: " + deleteBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createBook = new MyWebRequest(domain + "catalog/book/createOrUpdate", "POST", books);
                Console.WriteLine("createBook: " + createBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createAuthor = new MyWebRequest(domain + "catalog/author/createOrUpdate", "POST", authors);
                Console.WriteLine("createAuthor: " + createAuthor.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookAuthor = new MyWebRequest(domain + "catalog/attach/bookAuthor", "POST", bookAuthor);
                Console.WriteLine("attachBookAuthor: " + attachBookAuthor.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createKeyword = new MyWebRequest(domain + "catalog/keyword/createOrUpdate", "POST", keyword);
                Console.WriteLine("createKeyword: " + createKeyword.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookKeyword = new MyWebRequest(domain + "catalog/attach/bookKeyword", "POST", bookKeyword);
                Console.WriteLine("attachBookKeyword: " + attachBookKeyword.GetResponse());
                Console.WriteLine(new string('-', 60));
                Console.WriteLine(new string('-', 60));
            }
        }

        static string Title_MV(IrbisRecord record)
        {
            string title = null;
            bool flag = true;

            title = record.FM("200", 'A') + " [Электронный ресурс] " + record.FM("200", 'E') + " " + record.FM("923", 'H') + " " + record.FM("923", 'I') + " / ";
            if (record.FM("700") != null)
            {
                flag = false;
                title += record.FM("700", 'B') + " " + record.FM("700", 'A');
            }
            RecordField[] author_records = record.Fields.GetField("701").ToArray();
            foreach (var author_record in author_records)
            {
                if (flag)
                {
                    title += author_record.GetSubFieldText('B', 0) + " " + author_record.GetSubFieldText('A', 0);
                    flag = false;
                }
                else
                {
                    title += ", " + author_record.GetSubFieldText('B', 0) + " " + author_record.GetSubFieldText('A', 0);
                }
            }
            title += ". - " + record.FM("230", 'A') + ". - " + record.FM("210", 'A') + " : " + record.FM("210", 'C') + ", " + record.FM("210", 'D');
            title += ". - " + record.FM("215", 'A') + "с. : ";
            if (record.FM("215", 'C') != null)
            {
                title += record.FM("215", 'C').Remove(0, 2);
            }
            if (record.FM("215", '0') != null)
            {
                title += record.FM("215", '0').Remove(0, 2);
            }
            title += " - (" + record.FM("225", 'A') + ")" + record.FM("300");

            return title;
        }

        private static void MV(ManagedClient64 client)
        {
            // Делаем переключение на базу MV
            client.PushDatabase("MV");

            Console.WriteLine("PushDatabase MV");

            /*
             * Разбитие массива данных на чанки, и отправка в БД
             */

            int maxMfn = client.GetMaxMfn() - 1;
            int chunkLength = 25;
            int mfn = 0;
            Console.WriteLine("Total records: " + maxMfn);

            for (int loop = 0; loop < (int)Math.Ceiling((double)maxMfn / chunkLength); loop++)
            {
                /*
                 * Подготовка данных перед записью
                 */

                string books = "";
                string authors = "";
                string bookAuthor = "";
                string keyword = "";
                string bookKeyword = "";
                string bookDelete = "";
                string bookBranch = "";
                int j = 0;
                int k = 0;
                int l = 0;
                for (int i = 0; i < chunkLength; i++)
                {
                    mfn++;
                    if (mfn <= maxMfn)
                    {
                        IrbisRecord record = client.ReadRecord(mfn);

                        //Удаляем с бд книги с отсутствующими mfn
                        if (record == null)
                        {
                            bookDelete +=
                                "book[" + l + "][category_id]=" + "3" + "&" +
                                "book[" + l + "][mfn]=" + mfn + "&";
                            l++;
                        }
                        else
                        {
                            // Формируем чанк данных для переноса
                            books +=
                                "book[" + i + "][category_id]=" + "3" + "&" +
                                "book[" + i + "][mfn]=" + record.Mfn + "&" +
                                "book[" + i + "][name]=" + Title_MV(record) + "&" +
                                "book[" + i + "][year]=" + record.FM("210", 'd') + "&" +
                                "book[" + i + "][udk]=" + record.FM("675") + "&" +
                                "book[" + i + "][link]=" + "/MV" + record.FM("951", 'a') + "&" +
                                "book[" + i + "][link_name]=" + record.FM("951", 't') + "&";
                            if (record.FM("700", 'a') != null)
                            {
                                authors +=
                                    "author[" + j + "][surname]=" + record.FM("700", 'a') + "&" +
                                    "author[" + j + "][initials]=" + record.FM("700", 'b') + "&";
                                bookAuthor +=
                                    "attach[" + j + "][category_id]=" + "3" + "&" +
                                    "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + j + "][surname]=" + record.FM("700", 'a') + "&" +
                                    "attach[" + j + "][initials]=" + record.FM("700", 'b') + "&";
                                j++;
                            }
                            RecordField[] author_records = record.Fields.GetField("701").ToArray();
                            foreach (var author_record in author_records)
                            {
                                if (!((record.FM("700", 'a') == author_record.GetSubFieldText('a', 0)) &&
                                    (record.FM("700", 'b') == author_record.GetSubFieldText('b', 0))))
                                {
                                    authors +=
                                        "author[" + j + "][surname]=" + author_record.GetSubFieldText('a', 0) + "&" +
                                        "author[" + j + "][initials]=" + author_record.GetSubFieldText('b', 0) + "&";
                                    bookAuthor +=
                                        "attach[" + j + "][category_id]=" + "3" + "&" +
                                        "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                        "attach[" + j + "][surname]=" + author_record.GetSubFieldText('a', 0) + "&" +
                                        "attach[" + j + "][initials]=" + author_record.GetSubFieldText('b', 0) + "&";
                                    j++;
                                }
                            }
                            RecordField[] editor_records = record.Fields.GetField("702").ToArray();
                            foreach (var editor_record in editor_records)
                            {
                                authors +=
                                    "author[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "author[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                bookAuthor +=
                                    "attach[" + j + "][category_id]=" + "3" + "&" +
                                    "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "attach[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                j++;
                            }
                            RecordField[] keyword_records = record.Fields.GetField("610").ToArray();
                            foreach (var keyword_record in keyword_records)
                            {
                                keyword +=
                                    "keyword[" + k + "][value]=" + keyword_record.GetFieldText() + "&";
                                bookKeyword +=
                                    "attach[" + k + "][category_id]=" + "3" + "&" +
                                    "attach[" + k + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + k + "][value]=" + keyword_record.GetFieldText() + "&";
                                k++;
                            }
                            RecordField[] bookBranch_records = record.Fields.GetField("910").ToArray();
                            foreach (var bookBranch_record in bookBranch_records)
                            {
                                if (bookBranch_record.GetFirstSubFieldText('D') != null)
                                {
                                    bookBranch +=
                                    "attach[" + k + "][category_id]=" + "3" + "&" +
                                    "attach[" + k + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + k + "][branch_id]=" + Branch(bookBranch_record.GetFirstSubFieldText('D')) + "&";
                                    k++;
                                }
                            }
                        }
                    }
                }

                /*
                 * Методы отправки данных в БД
                 */
                MyWebRequest deleteBook = new MyWebRequest(domain + "catalog/book/delete", "POST", bookDelete);
                Console.WriteLine("deleteBook: " + deleteBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createBook = new MyWebRequest(domain + "catalog/book/createOrUpdate", "POST", books);
                Console.WriteLine("createBook: " + createBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createAuthor = new MyWebRequest(domain + "catalog/author/createOrUpdate", "POST", authors);
                Console.WriteLine("createAuthor: " + createAuthor.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookAuthor = new MyWebRequest(domain + "catalog/attach/bookAuthor", "POST", bookAuthor);
                Console.WriteLine("attachBookAuthor: " + attachBookAuthor.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createKeyword = new MyWebRequest(domain + "catalog/keyword/createOrUpdate", "POST", keyword);
                Console.WriteLine("createKeyword: " + createKeyword.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookKeyword = new MyWebRequest(domain + "catalog/attach/bookKeyword", "POST", bookKeyword);
                Console.WriteLine("attachBookKeyword: " + attachBookKeyword.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookBranch = new MyWebRequest(domain + "catalog/attach/bookBranch", "POST", bookBranch);
                Console.WriteLine("attachBookBranch: " + attachBookBranch.GetResponse());
                Console.WriteLine(new string('-', 60));
                Console.WriteLine(new string('-', 60));
            }
        }

        static string Title_DB2(IrbisRecord record)
        {
            string title = null;

            if (record.FM("200", 'A') != null)
            {
                title += record.FM("200", 'A');
            }

            if (record.FM("200", 'E') != null)
            {
                title += " : " + record.FM("200", 'E');
            }

            if (record.FM("923", 'I') != null)
            {
                title += " " + record.FM("923", 'H') + ". " + record.FM("923", 'I');
            }

            if (record.FM("200", 'F') != null)
            {
                title += " / " + record.FM("200", 'F');
            }

            if (record.FM("461", 'C') != null)
            {
                title += record.FM("461", 'C');
            }

            if (record.FM("461", 'E') != null)
            {
                title += " : " + record.FM("461", 'E');
            }

            if (record.FM("461", 'F') != null)
            {
                title += " / " + record.FM("461", 'F');
            }

            if (record.FM("461", 'D') != null)
            {
                title += ". - " + record.FM("461", 'D');
            }

            if (record.FM("200", 'V') != null)
            {
                title += " : " + record.FM("200", 'V');
            }

            if (record.FM("205", 'A') != null)
            {
                title += ". - " + record.FM("205", 'A');
            }

            if (record.FM("436", 'C') != null)
            {
                title += " // " + record.FM("436", 'C');
            }

            if (record.FM("963", 'E') != null)
            {
                title += " : " + record.FM("963", 'E');
            }

            if (record.FM("963", 'F') != null)
            {
                title += " / " + record.FM("963", 'F');
            }

            if (record.FM("463", 'G') != null)
            {
                title += ". - Д. : " + record.FM("463", 'G');
            }

            if (record.FM("463", 'G') != null)
            {
                title += ". - Д. : " + record.FM("463", 'G') + ", = " + record.FM("463", 'R');
            }

            if (record.FM("210", 'A') != null)
            {
                title += ". - " + record.FM("210", 'A');
                if (record.FM("210", 'C') != null)
                {
                    title += " : " + record.FM("210", 'C') + ", " + record.FM("210", 'D');
                }
                else
                {
                    title += " : " + "[б. и.]" + ", " + record.FM("210", 'D');
                }
            }

            if (record.FM("210", 'D') != null)
            {
                title += ". - " + record.FM("210", 'D');
            }

            if (record.FM("436", 'S') != null)
            {
                title += ". - С. " + record.FM("463", 'S');
            }

            if (record.FM("215", 'A') != null)
            {
                title += ". - " + record.FM("215", 'A') + " с.";
            }

            if (record.FM("225", 'A') != null)
            {
                title += " - (" + record.FM("225", 'A') + ")";
            }

            return title;
        }

        private static void DB2(ManagedClient64 client)
        {
            // Делаем переключение на базу DB2
            client.PushDatabase("DB2");

            Console.WriteLine("PushDatabase DB2");

            /*
             * Разбитие массива данных на чанки, и отправка в БД
             */

            int maxMfn = client.GetMaxMfn() - 1;
            int chunkLength = 25;
            int mfn = 0;
            Console.WriteLine("Total records: " + maxMfn);

            for (int loop = 0; loop < (int)Math.Ceiling((double)maxMfn / chunkLength); loop++)
            {
                /*
                    * Подготовка данных перед записью
                    */

                string books = "";
                string authors = "";
                string bookAuthor = "";
                string keyword = "";
                string bookKeyword = "";
                string bookDelete = "";
                string bookBranch = "";
                int j = 0;
                int k = 0;
                int l = 0;
                for (int i = 0; i < chunkLength; i++)
                {
                    mfn++;
                    if (mfn <= maxMfn)
                    {
                        IrbisRecord record = client.ReadRecord(mfn);

                        //Удаляем с бд книги с отсутствующими mfn
                        if (record == null)
                        {
                            bookDelete +=
                                "book[" + l + "][category_id]=" + "4" + "&" +
                                "book[" + l + "][mfn]=" + mfn + "&";
                            l++;
                        }
                        else
                        {
                            // Формируем чанк данных для переноса
                            books +=
                                "book[" + i + "][category_id]=" + "4" + "&" +
                                "book[" + i + "][mfn]=" + record.Mfn + "&" +
                                "book[" + i + "][name]=" + Title_DB2(record) + "&" +
                                "book[" + i + "][year]=" + record.FM("210", 'd') + "&" +
                                "book[" + i + "][udk]=" + record.FM("675") + "&" +
                                "book[" + i + "][link]=" + record.FM("951", 'a') + "&" +
                                "book[" + i + "][link_name]=" + record.FM("951", 't') + "&";

                            if (record.FM("700", 'a') != null)
                            {
                                authors +=
                                    "author[" + j + "][surname]=" + record.FM("700", 'a') + "&" +
                                    "author[" + j + "][initials]=" + record.FM("700", 'b') + "&";
                                bookAuthor +=
                                    "attach[" + j + "][category_id]=" + "4" + "&" +
                                    "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + j + "][surname]=" + record.FM("700", 'a') + "&" +
                                    "attach[" + j + "][initials]=" + record.FM("700", 'b') + "&";
                                j++;
                            }
                            RecordField[] author_records = record.Fields.GetField("701").ToArray();
                            foreach (var author_record in author_records)
                            {
                                if (!((record.FM("700", 'a') == author_record.GetSubFieldText('a', 0)) &&
                                    (record.FM("700", 'b') == author_record.GetSubFieldText('b', 0))))
                                {
                                    authors +=
                                        "author[" + j + "][surname]=" + author_record.GetSubFieldText('a', 0) + "&" +
                                        "author[" + j + "][initials]=" + author_record.GetSubFieldText('b', 0) + "&";
                                    bookAuthor +=
                                        "attach[" + j + "][category_id]=" + "4" + "&" +
                                        "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                        "attach[" + j + "][surname]=" + author_record.GetSubFieldText('a', 0) + "&" +
                                        "attach[" + j + "][initials]=" + author_record.GetSubFieldText('b', 0) + "&";
                                    j++;
                                }
                            }
                            RecordField[] editor_records = record.Fields.GetField("702").ToArray();
                            foreach (var editor_record in editor_records)
                            {
                                authors +=
                                    "author[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "author[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                bookAuthor +=
                                    "attach[" + j + "][category_id]=" + "4" + "&" +
                                    "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "attach[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                j++;
                            }
                            RecordField[] editor_recordss = record.Fields.GetField("961").ToArray();
                            foreach (var editor_record in editor_recordss)
                            {
                                authors +=
                                    "author[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "author[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                bookAuthor +=
                                    "attach[" + j + "][category_id]=" + "4" + "&" +
                                    "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "attach[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                j++;
                            }
                            RecordField[] keyword_records = record.Fields.GetField("610").ToArray();
                            foreach (var keyword_record in keyword_records)
                            {
                                keyword +=
                                    "keyword[" + k + "][value]=" + keyword_record.GetFieldText() + "&";
                                bookKeyword +=
                                    "attach[" + k + "][category_id]=" + "4" + "&" +
                                    "attach[" + k + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + k + "][value]=" + keyword_record.GetFieldText() + "&";
                                k++;
                            }
                            RecordField[] bookBranch_records = record.Fields.GetField("910").ToArray();
                            foreach (var bookBranch_record in bookBranch_records)
                            {
                                if (bookBranch_record.GetFirstSubFieldText('D') != null )
                                {
                                    bookBranch +=
                                    "attach[" + k + "][category_id]=" + "4" + "&" +
                                    "attach[" + k + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + k + "][branch_id]=" + Branch(bookBranch_record.GetFirstSubFieldText('D')) + "&";
                                    k++;
                                }
                            }
                        }
                    }
                }

                /*
                    * Методы отправки данных в БД
                    */
                MyWebRequest deleteBook = new MyWebRequest(domain + "catalog/book/delete", "POST", bookDelete);
                Console.WriteLine("deleteBook: " + deleteBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createBook = new MyWebRequest(domain + "catalog/book/createOrUpdate", "POST", books);
                Console.WriteLine("createBook: " + createBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createAuthor = new MyWebRequest(domain + "catalog/author/createOrUpdate", "POST", authors);
                Console.WriteLine("createAuthor: " + createAuthor.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookAuthor = new MyWebRequest(domain + "catalog/attach/bookAuthor", "POST", bookAuthor);
                Console.WriteLine("attachBookAuthor: " + attachBookAuthor.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createKeyword = new MyWebRequest(domain + "catalog/keyword/createOrUpdate", "POST", keyword);
                Console.WriteLine("createKeyword: " + createKeyword.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookKeyword = new MyWebRequest(domain + "catalog/attach/bookKeyword", "POST", bookKeyword);
                Console.WriteLine("attachBookKeyword: " + attachBookKeyword.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookBranch = new MyWebRequest(domain + "catalog/attach/bookBranch", "POST", bookBranch);
                Console.WriteLine("attachBookBranch: " + attachBookBranch.GetResponse());
                Console.WriteLine(new string('-', 60));
                Console.WriteLine(new string('-', 60));
            }
        }

        static string Title_PVUD(IrbisRecord record, IrbisRecord record2)
        {
            string title = null;

            if (record2.FM("200", 'A') != null)
            {
                title += record2.FM("200", 'A');
            }

            if (record2.FM("200", 'E') != null)
            {
                title += " : " + record2.FM("200", 'E');
            }

            if (record2.FM("210", 'A') != null)
            {
                title += ". - " + record2.FM("210", 'A');
            }

            if (record2.FM("210", 'C') != null)
            {
                title += " : " + record2.FM("210", 'C');
            }

            if (record2.FM("210", 'D') != null)
            {
                title += ". - " + record2.FM("210", 'D');
            }

            if (record2.FM("110", 'D') != null)
            {
                title += ". - Выходит ";
                switch (record2.FM("110", 'D'))
                {
                    case "a":
                        title += "ежедневно";
                        break;
                    case "b":
                        title += "дважды в неделю";
                        break;
                    case "c":
                        title += "еженедельно";
                        break;
                    case "d":
                        title += "раз в две недели";
                        break;
                    case "e":
                        title += "дважды в месяц";
                        break;
                    case "f":
                        title += "ежемесячно";
                        break;
                    case "10":
                        title += "10 в год";
                        break;
                    case "8":
                        title += "8 в год";
                        break;
                    case "7":
                        title += "7 в год";
                        break;
                    case "g":
                        title += "раз в два месяца";
                        break;
                    case "h":
                        title += "ежеквартально";
                        break;
                    case "i":
                        title += "три раза в год";
                        break;
                    case "j":
                        title += "дважды в год";
                        break;
                    case "k":
                        title += "ежегодно";
                        break;
                    case "l":
                        title += "раз в два года";
                        break;
                    case "m":
                        title += "раз в три года";
                        break;
                    case "n":
                        title += "три раза в неделю";
                        break;
                    case "208":
                        title += "четыре раза в неделю";
                        break;
                    case "o":
                        title += "три раза в месяц";
                        break;
                    case "u":
                        title += "неизвестно";
                        break;
                    case "y":
                        title += "нерегулярно";
                        break;
                    case "z":
                        title += "другая";
                        break;
                }
            }

            if (record.FM("934") != null)
            {
                title += " " + record.FM("934") + "г.";
            }

            if (record.FM("931", '9') != null)
            {
                title += " " + record.FM("931", '9') + " ";
            }

            if (record.FM("936") != null)
            {
                title += record.FM("936");
            }

            return title;
        }

        private static void PVUD(ManagedClient64 client)
        {
            // Делаем переключение на базу PVUD
            client.PushDatabase("PVUD");

            Console.WriteLine("PushDatabase PVUD");

            /*
             * Разбитие данных на чанки, и отправка в БД
             */

            int maxMfn = client.GetMaxMfn() - 1;
            int chunkLength = 25;
            int mfn = 0;
            Console.WriteLine("Total records: " + maxMfn);
            for (int loop = 0; loop < (int)Math.Ceiling((double)maxMfn / chunkLength); loop++)
            {
                /*
                * Подготовка данных перед записью
                */

                string books = "";
                string bookDelete = "";
                string bookBranch = "";
                int l = 0;
                int k = 0;

                for (int i = 0; i < chunkLength; i++)
                {
                    mfn++;
                    if (mfn <= maxMfn)
                    {
                        IrbisRecord record = client.ReadRecord(mfn);
                        if (record == null || record.FM("933") == null || record.FM("903").Length == 3)
                        {
                            //Удаляем с бд книги с отсутствующими mfn
                            bookDelete +=
                                "book[" + l + "][category_id]=" + "5" + "&" +
                                "book[" + l + "][mfn]=" + mfn + "&";
                            l++;
                        }
                        else
                        {
                            // Ищем журнал
                            bool fl = true;
                            int mfn2 = 0;
                            IrbisRecord record2 = null;
                            while (fl && (mfn2 <= maxMfn))
                            {
                                mfn2++;
                                record2 = client.ReadRecord(mfn2);
                                if (record.FM("933") == record2.FM("903"))
                                {
                                    fl = false;
                                }
                            }
                            // Формируем чанк данных для переноса
                            books +=
                                "book[" + i + "][category_id]=" + "5" + "&" +
                                "book[" + i + "][mfn]=" + record.Mfn + "&" +
                                "book[" + i + "][name]=" + Title_PVUD(record, record2) + "&" +
                                "book[" + i + "][year]=" + record.FM("934") + "&" +
                                "book[" + i + "][udk]=" + record.FM("903") + "&" +
                                "book[" + i + "][link]=" + "" + "&" +
                                "book[" + i + "][link_name]=" + "" + "&";
                        }
                        RecordField[] bookBranch_records = record.Fields.GetField("910").ToArray();
                        foreach (var bookBranch_record in bookBranch_records)
                        {
                            if (bookBranch_record.GetFirstSubFieldText('D') != null)
                            {
                                bookBranch +=
                                "attach[" + k + "][category_id]=" + "5" + "&" +
                                "attach[" + k + "][mfn]=" + record.Mfn + "&" +
                                "attach[" + k + "][branch_id]=" + Branch(bookBranch_record.GetFirstSubFieldText('D')) + "&";
                                k++;
                            }
                        }
                    }
                }
                /*
                * Методы отправки данных в БД
                */
                MyWebRequest deleteBook = new MyWebRequest(domain + "catalog/book/delete", "POST", bookDelete);
                Console.WriteLine("deleteBook: " + deleteBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createBook = new MyWebRequest(domain + "catalog/book/createOrUpdate", "POST", books);
                Console.WriteLine("createBook: " + createBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookBranch = new MyWebRequest(domain + "catalog/attach/bookBranch", "POST", bookBranch);
                Console.WriteLine("attachBookBranch: " + attachBookBranch.GetResponse());
                Console.WriteLine(new string('-', 60));
                Console.WriteLine(new string('-', 60));
            }
        }

        static string Title_DB4(IrbisRecord record)
        {
            string title = null;

            title = record.FM("200", 'A');

            if (record.FM("200", 'E') != null)
            {
                title += " : " + record.FM("200", 'E');
            }

            if (record.FM("923", 'I') != null)
            {
                title += " " + record.FM("923", 'H') + ". " + record.FM("923", 'I');
            }

            title += " / " + record.FM("200", 'F');

            if (record.FM("205", 'A') != null)
            {
                title += ". - " + record.FM("205", 'A');
            }

            if (record.FM("436", 'C') != null)
            {
                title += " // " + record.FM("436", 'C');
            }

            if (record.FM("963", 'E') != null)
            {
                title += " : " + record.FM("963", 'E');
            }

            if (record.FM("963", 'F') != null)
            {
                title += " / " + record.FM("963", 'F');
            }

            if (record.FM("463", 'C') != null)
            {
                title += " // " + record.FM("463", 'C') + ".";
            }

            if (record.FM("463", 'J') != null)
            {
                title += " - " + record.FM("463", 'J') + ".";
            }

            if (record.FM("463", 'V') != null)
            {
                title += " - " + record.FM("463", 'V') + ".";
            }

            if (record.FM("463", 'H') != null)
            {
                title += " - " + record.FM("463", 'H') + ".";
            }

            if (record.FM("463", 'S') != null)
            {
                title += " - С. " + record.FM("463", 'S') + ".";
            }

            if (record.FM("463", 'G') != null)
            {
                title += ". - Д. : " + record.FM("463", 'G');
            }

            if (record.FM("463", 'G') != null)
            {
                title += ". - Д. : " + record.FM("463", 'G') + ", = " + record.FM("463", 'R');
            }

            if (record.FM("210", 'A') != null)
            {
                title += ". - " + record.FM("210", 'A');
                if (record.FM("210", 'C') != null)
                {
                    title += " : " + record.FM("210", 'C') + ", " + record.FM("210", 'D');
                }
                else
                {
                    title += " : " + "[б. и.]" + ", " + record.FM("210", 'D');
                }
            }

            if (record.FM("436", 'S') != null)
            {
                title += ". - С. " + record.FM("463", 'S');
            }

            if (record.FM("215", 'A') != null)
            {
                title += ". - " + record.FM("215", 'A') + " с.";
            }

            if (record.FM("225", 'A') != null)
            {
                title += " - (" + record.FM("225", 'A') + ")";
            }

            return title;
        }

        static string Title_DB4(IrbisRecord record, IrbisRecord record2)
        {
            string title = null;

            if (record2.FM("200", 'A') != null)
            {
                title += record2.FM("200", 'A');
            }

            if (record2.FM("200", 'E') != null)
            {
                title += " : " + record2.FM("200", 'E');
            }

            if (record2.FM("210", 'A') != null)
            {
                title += ". - " + record2.FM("210", 'A');
            }

            if (record2.FM("210", 'C') != null)
            {
                title += " : " + record2.FM("210", 'C');
            }

            if (record2.FM("210", 'D') != null)
            {
                title += ". - " + record2.FM("210", 'D');
            }

            if (record2.FM("110", 'D') != null)
            {
                title += ". - Выходит ";
                switch (record2.FM("110", 'D'))
                {
                    case "a":
                        title += "ежедневно";
                        break;
                    case "b":
                        title += "дважды в неделю";
                        break;
                    case "c":
                        title += "еженедельно";
                        break;
                    case "d":
                        title += "раз в две недели";
                        break;
                    case "e":
                        title += "дважды в месяц";
                        break;
                    case "f":
                        title += "ежемесячно";
                        break;
                    case "10":
                        title += "10 в год";
                        break;
                    case "8":
                        title += "8 в год";
                        break;
                    case "7":
                        title += "7 в год";
                        break;
                    case "g":
                        title += "раз в два месяца";
                        break;
                    case "h":
                        title += "ежеквартально";
                        break;
                    case "i":
                        title += "три раза в год";
                        break;
                    case "j":
                        title += "дважды в год";
                        break;
                    case "k":
                        title += "ежегодно";
                        break;
                    case "l":
                        title += "раз в два года";
                        break;
                    case "m":
                        title += "раз в три года";
                        break;
                    case "n":
                        title += "три раза в неделю";
                        break;
                    case "208":
                        title += "четыре раза в неделю";
                        break;
                    case "o":
                        title += "три раза в месяц";
                        break;
                    case "u":
                        title += "неизвестно";
                        break;
                    case "y":
                        title += "нерегулярно";
                        break;
                    case "z":
                        title += "другая";
                        break;
                }
            }

            if (record.FM("934") != null)
            {
                title += " " + record.FM("934") + "г.";
            }

            if (record.FM("931", '9') != null)
            {
                title += " " + record.FM("931", '9') + " ";
            }

            if (record.FM("936") != null)
            {
                title += " №" + record.FM("936");
            }

            return title;
        }

        private static void DB4(ManagedClient64 client)
        {
            // Делаем переключение на базу DB4
            client.PushDatabase("DB4");

            Console.WriteLine("PushDatabase DB4");

            /*
             * Разбитие массива данных на чанки, и отправка в БД
             */

            int maxMfn = client.GetMaxMfn() - 1;
            int chunkLength = 25;
            int mfn = 0;
            Console.WriteLine("Total records: " + maxMfn);

            for (int loop = 0; loop < (int)Math.Ceiling((double)maxMfn / chunkLength); loop++)
            {
                /*
                 * Подготовка данных перед записью
                 */

                string books = "";
                string authors = "";
                string bookAuthor = "";
                string keyword = "";
                string bookKeyword = "";
                string bookDelete = "";
                string bookBranch = "";
                int j = 0;
                int k = 0;
                int l = 0;
                for (int i = 0; i < chunkLength - 1; i++)
                {
                    mfn++;
                    if (mfn <= maxMfn)
                    {
                        IrbisRecord record = client.ReadRecord(mfn);

                        //Удаляем с бд книги с отсутствующими mfn
                        if (record == null || record.FM("903").Length == 3 || record.FM("903").Length == 2)
                        {
                            bookDelete +=
                                "book[" + l + "][category_id]=" + "6" + "&" +
                                "book[" + l + "][mfn]=" + mfn + "&";
                            l++;
                        }
                        else
                        {
                            if (record.FM("933") != null)
                            {
                                bool fl = true;
                                int mfn2 = 0;
                                IrbisRecord record2 = null;
                                while (fl && (mfn2 <= maxMfn))
                                {
                                    mfn2++;
                                    record2 = client.ReadRecord(mfn2);
                                    if (record.FM("933") == record2.FM("903"))
                                    {
                                        fl = false;
                                    }
                                }
                                // Формируем чанк данных для переноса
                                books +=
                                    "book[" + i + "][category_id]=" + "6" + "&" +
                                    "book[" + i + "][mfn]=" + record.Mfn + "&" +
                                    "book[" + i + "][name]=" + Title_DB4(record, record2) + "&" +
                                    "book[" + i + "][year]=" + record.FM("934") + "&" +
                                    "book[" + i + "][udk]=" + record.FM("675") + "&" +
                                    "book[" + i + "][link]=" + record.FM("951", 'a') + "&" +
                                    "book[" + i + "][link_name]=" + record.FM("951", 't') + "&";
                            }

                            if (record.FM("933") == null)
                            {
                                // Формируем чанк данных для переноса
                                books +=
                                    "book[" + i + "][category_id]=" + "6" + "&" +
                                    "book[" + i + "][mfn]=" + record.Mfn + "&" +
                                    "book[" + i + "][name]=" + Title_DB4(record) + "&" +
                                    "book[" + i + "][year]=" + record.FM("463", 'j') + "&" +
                                    "book[" + i + "][udk]=" + record.FM("675") + "&" +
                                    "book[" + i + "][link]=" + record.FM("951", 'a') + "&" +
                                    "book[" + i + "][link_name]=" + record.FM("951", 't') + "&";
                            }

                            if (record.FM("700", 'a') != null)
                            {
                                authors +=
                                    "author[" + j + "][surname]=" + record.FM("700", 'a') + "&" +
                                    "author[" + j + "][initials]=" + record.FM("700", 'b') + "&";
                                bookAuthor +=
                                    "attach[" + j + "][category_id]=" + "6" + "&" +
                                    "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + j + "][surname]=" + record.FM("700", 'a') + "&" +
                                    "attach[" + j + "][initials]=" + record.FM("700", 'b') + "&";
                                j++;
                            }
                            RecordField[] author_records = record.Fields.GetField("701").ToArray();
                            foreach (var author_record in author_records)
                            {
                                if (!((record.FM("700", 'a') == author_record.GetSubFieldText('a', 0)) &&
                                    (record.FM("700", 'b') == author_record.GetSubFieldText('b', 0))))
                                {
                                    authors +=
                                        "author[" + j + "][surname]=" + author_record.GetSubFieldText('a', 0) + "&" +
                                        "author[" + j + "][initials]=" + author_record.GetSubFieldText('b', 0) + "&";
                                    bookAuthor +=
                                        "attach[" + j + "][category_id]=" + "6" + "&" +
                                        "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                        "attach[" + j + "][surname]=" + author_record.GetSubFieldText('a', 0) + "&" +
                                        "attach[" + j + "][initials]=" + author_record.GetSubFieldText('b', 0) + "&";
                                    j++;
                                }
                            }
                            RecordField[] editor_records = record.Fields.GetField("702").ToArray();
                            foreach (var editor_record in editor_records)
                            {
                                authors +=
                                    "author[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "author[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                bookAuthor +=
                                    "attach[" + j + "][category_id]=" + "6" + "&" +
                                    "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "attach[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                j++;
                            }
                            RecordField[] keyword_records = record.Fields.GetField("610").ToArray();
                            foreach (var keyword_record in keyword_records)
                            {
                                keyword +=
                                    "keyword[" + k + "][value]=" + keyword_record.GetFieldText() + "&";
                                bookKeyword +=
                                    "attach[" + k + "][category_id]=" + "6" + "&" +
                                    "attach[" + k + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + k + "][value]=" + keyword_record.GetFieldText() + "&";
                                k++;
                            }
                            RecordField[] bookBranch_records = record.Fields.GetField("910").ToArray();
                            foreach (var bookBranch_record in bookBranch_records)
                            {
                                if (bookBranch_record.GetFirstSubFieldText('D') != null)
                                {
                                    bookBranch +=
                                    "attach[" + k + "][category_id]=" + "6" + "&" +
                                    "attach[" + k + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + k + "][branch_id]=" + Branch(bookBranch_record.GetFirstSubFieldText('D')) + "&";
                                    k++;
                                }
                            }
                        }
                    }
                }

                /*
                 * Методы отправки данных в БД
                 */
                MyWebRequest deleteBook = new MyWebRequest(domain + "catalog/book/delete", "POST", bookDelete);
                Console.WriteLine("deleteBook: " + deleteBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createBook = new MyWebRequest(domain + "catalog/book/createOrUpdate", "POST", books);
                Console.WriteLine("createBook: " + createBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createAuthor = new MyWebRequest(domain + "catalog/author/createOrUpdate", "POST", authors);
                Console.WriteLine("createAuthor: " + createAuthor.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookAuthor = new MyWebRequest(domain + "catalog/attach/bookAuthor", "POST", bookAuthor);
                Console.WriteLine("attachBookAuthor: " + attachBookAuthor.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createKeyword = new MyWebRequest(domain + "catalog/keyword/createOrUpdate", "POST", keyword);
                Console.WriteLine("createKeyword: " + createKeyword.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookKeyword = new MyWebRequest(domain + "catalog/attach/bookKeyword", "POST", bookKeyword);
                Console.WriteLine("attachBookKeyword: " + attachBookKeyword.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookBranch = new MyWebRequest(domain + "catalog/attach/bookBranch", "POST", bookBranch);
                Console.WriteLine("attachBookBranch: " + attachBookBranch.GetResponse());
                Console.WriteLine(new string('-', 60));
                Console.WriteLine(new string('-', 60));
            }
        }

        static string Title_RARIT(IrbisRecord record)
        {
            string title = null;

            if (record.FM("461", 'C') != null)
            {
                title += record.FM("461", 'C');
            }

            if (record.FM("461", 'E') != null)
            {
                title += ": " + record.FM("461", 'E');
            }

            if (record.FM("461", 'F') != null)
            {
                title += " / " + record.FM("461", 'F');
            }

            if (record.FM("461", 'D') != null)
            {
                title += ". - " + record.FM("461", 'D') + " : ";
            }

            if (record.FM("461", 'G') != null)
            {
                title += record.FM("461", 'G');
            }

            if (record.FM("200", 'V') != null)
            {
                title += " " + record.FM("200", 'V') + " : ";
            }

            if (record.FM("200", 'A') != null)
            {
                title += record.FM("200", 'A');
            }

            if (record.FM("200", 'E') != null)
            {
                title += " : " + record.FM("200", 'A');
            }

            if (record.FM("700", 'B') != null)
            {
                title += " / " + record.FM("700", 'B');
            }

            if (record.FM("700", 'A') != null)
            {
                title += " " + record.FM("700", 'A');
            }

            if (record.FM("210", 'D') != null)
            {
                title += ". - " + record.FM("210", 'D') + ". - ";
            }

            if (record.FM("215", 'A') != null)
            {
                title += record.FM("215", 'A') + " c.";
            }

            return title;
        }

        private static void RARIT(ManagedClient64 client)
        {
            // Делаем переключение на базу RARIT
            client.PushDatabase("RARIT");

            Console.WriteLine("PushDatabase RARIT");

            /*
             * Разбитие данных на чанки, и отправка в БД
             */

            int maxMfn = client.GetMaxMfn() - 1;
            int chunkLength = 25;
            int mfn = 0;
            Console.WriteLine("Total records: " + maxMfn);

            for (int loop = 0; loop < (int)Math.Ceiling((double)maxMfn / chunkLength); loop++)
            {
                /*
                * Подготовка данных перед записью
                */

                string books = "";
                string authors = "";
                string bookAuthor = "";
                string keyword = "";
                string bookKeyword = "";
                string bookDelete = "";
                string bookBranch = "";
                int j = 0;
                int k = 0;
                int l = 0;

                for (int i = 0; i < chunkLength; i++)
                {
                    mfn++;
                    if (mfn <= maxMfn)
                    {
                        IrbisRecord record = client.ReadRecord(mfn);
                        if (record == null)
                        {
                            //Удаляем с бд книги с отсутствующими mfn
                            for (int m = mfn + 1; m < record.Mfn; m++)
                            {
                                bookDelete +=
                                    "book[" + l + "][category_id]=" + "7" + "&" +
                                    "book[" + l + "][mfn]=" + m + "&";
                                l++;
                            }
                        }
                        else
                        {
                            // Формируем чанк данных для переноса
                            books +=
                                "book[" + i + "][category_id]=" + "7" + "&" +
                                "book[" + i + "][mfn]=" + record.Mfn + "&" +
                                "book[" + i + "][name]=" + Title_RARIT(record) + "&" +
                                "book[" + i + "][year]=" + record.FM("210", 'd') + "&" +
                                "book[" + i + "][udk]=" + record.FM("675") + "&" +
                                "book[" + i + "][link]=" + record.FM("951", 'a') + "&" +
                                "book[" + i + "][link_name]=" + record.FM("951", 't') + "&";

                            if (record.FM("700", 'a') != null)
                            {
                                authors +=
                                    "author[" + j + "][surname]=" + record.FM("700", 'a') + "&" +
                                    "author[" + j + "][initials]=" + record.FM("700", 'b') + "&";
                                bookAuthor +=
                                    "attach[" + j + "][category_id]=" + "7" + "&" +
                                    "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + j + "][surname]=" + record.FM("700", 'a') + "&" +
                                    "attach[" + j + "][initials]=" + record.FM("700", 'b') + "&";
                                j++;
                            }
                            RecordField[] author_records = record.Fields.GetField("701").ToArray();
                            foreach (var author_record in author_records)
                            {
                                if (!((record.FM("700", 'a') == author_record.GetSubFieldText('a', 0)) &&
                                    (record.FM("700", 'b') == author_record.GetSubFieldText('b', 0))))
                                {
                                    authors +=
                                        "author[" + j + "][surname]=" + author_record.GetSubFieldText('a', 0) + "&" +
                                        "author[" + j + "][initials]=" + author_record.GetSubFieldText('b', 0) + "&";
                                    bookAuthor +=
                                        "attach[" + j + "][category_id]=" + "7" + "&" +
                                        "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                        "attach[" + j + "][surname]=" + author_record.GetSubFieldText('a', 0) + "&" +
                                        "attach[" + j + "][initials]=" + author_record.GetSubFieldText('b', 0) + "&";
                                    j++;
                                }
                            }
                            RecordField[] editor_records = record.Fields.GetField("702").ToArray();
                            foreach (var editor_record in editor_records)
                            {
                                authors +=
                                    "author[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "author[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                bookAuthor +=
                                    "attach[" + j + "][category_id]=" + "7" + "&" +
                                    "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "attach[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                j++;
                            }
                            RecordField[] keyword_records = record.Fields.GetField("610").ToArray();
                            foreach (var keyword_record in keyword_records)
                            {
                                keyword +=
                                    "keyword[" + k + "][value]=" + keyword_record.GetFieldText() + "&";
                                bookKeyword +=
                                    "attach[" + k + "][category_id]=" + "7" + "&" +
                                    "attach[" + k + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + k + "][value]=" + keyword_record.GetFieldText() + "&";
                                k++;
                            }
                            RecordField[] bookBranch_records = record.Fields.GetField("910").ToArray();
                            foreach (var bookBranch_record in bookBranch_records)
                            {
                                if (bookBranch_record.GetFirstSubFieldText('D') != null)
                                {
                                    bookBranch +=
                                    "attach[" + k + "][category_id]=" + "7" + "&" +
                                    "attach[" + k + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + k + "][branch_id]=" + Branch(bookBranch_record.GetFirstSubFieldText('D')) + "&";
                                    k++;
                                }
                            }
                        }

                    }
                }
                /*
                * Методы отправки данных в БД
                */
                MyWebRequest deleteBook = new MyWebRequest(domain + "catalog/book/delete", "POST", bookDelete);
                Console.WriteLine("deleteBook: " + deleteBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createBook = new MyWebRequest(domain + "catalog/book/createOrUpdate", "POST", books);
                Console.WriteLine("createBook: " + createBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createAuthor = new MyWebRequest(domain + "catalog/author/createOrUpdate", "POST", authors);
                Console.WriteLine("createAuthor: " + createAuthor.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookAuthor = new MyWebRequest(domain + "catalog/attach/bookAuthor", "POST", bookAuthor);
                Console.WriteLine("attachBookAuthor: " + attachBookAuthor.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createKeyword = new MyWebRequest(domain + "catalog/keyword/createOrUpdate", "POST", keyword);
                Console.WriteLine("createKeyword: " + createKeyword.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookKeyword = new MyWebRequest(domain + "catalog/attach/bookKeyword", "POST", bookKeyword);
                Console.WriteLine("attachBookKeyword: " + attachBookKeyword.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookBranch = new MyWebRequest(domain + "catalog/attach/bookBranch", "POST", bookBranch);
                Console.WriteLine("attachBookBranch: " + attachBookBranch.GetResponse());
                Console.WriteLine(new string('-', 60));
                Console.WriteLine(new string('-', 60));
            }
        }

        static string Title_PB(IrbisRecord record)
        {
            string title = null;

            if (record.FM("982", '0') != null)
            {
                title += record.FM("982", '0');
            }

            if (record.FM("982", '9') != null)
            {
                title += " " + record.FM("982", '9');
            }

            if (record.FM("982", 'S') != null)
            {
                title += " " + record.FM("982", 'S');
            }

            if (record.FM("982", 'I') != null)
            {
                title += ", " + record.FM("982", 'I') + ".";
            }

            if (record.FM("200", 'A') != null)
            {
                title += " " + record.FM("200", 'A');
            }

            if (record.FM("200", 'E') != null)
            {
                title += " : " + record.FM("200", 'E') + " / ";
            }

            if (record.FM("200", 'F') != null)
            {
                title += record.FM("200", 'F') + " ; ";
            }

            if (record.FM("200", 'G') != null)
            {
                title += record.FM("200", 'G') + ". - ";
            }

            if (record.FM("982", 'Z') != null)
            {
                title += " № " + record.FM("982", 'Z') + "; ";
            }

            if (record.FM("982", 'R') != null)
            {
                title += "Заявл. " + record.FM("982", 'R')[6] + record.FM("982", 'R')[7] + "." +
                    record.FM("982", 'R')[4] + record.FM("982", 'R')[5] + "." +
                    record.FM("982", 'R')[0] + record.FM("982", 'R')[1] +
                    record.FM("982", 'R')[2] + record.FM("982", 'R')[3] + "; ";
            }

            if (record.FM("982", 'P') != null)
            {
                title += "Опубл. " + record.FM("982", 'P')[6] + record.FM("982", 'P')[7] + "." +
                    record.FM("982", 'P')[4] + record.FM("982", 'P')[5] + "." +
                    record.FM("982", 'P')[0] + record.FM("982", 'P')[1] +
                    record.FM("982", 'P')[2] + record.FM("982", 'P')[3] + ", ";
            }

            if (record.FM("982", 'M') != null)
            {
                title += record.FM("982", 'M') + ". - ";
            }

            if (record.FM("982", 'B') != null)
            {
                title += record.FM("982", 'B') + " ";
            }

            if (record.FM("982", 'C') != null)
            {
                title += "по " + record.FM("982", 'C');
            }

            if (record.FM("215", 'A') != null)
            {
                title += ". - " + record.FM("215", 'A') + "с.";
            }

            return title;
        }

        private static void PB(ManagedClient64 client)
        {
            // Делаем переключение на базу PB
            client.PushDatabase("PB");

            Console.WriteLine("PushDatabase PB");

            /*
             * Разбитие данных на чанки, и отправка в БД
             */

            int maxMfn = client.GetMaxMfn() - 1;
            int chunkLength = 25;
            int mfn = 0;
            Console.WriteLine("Total records: " + maxMfn);

            for (int loop = 0; loop < (int)Math.Ceiling((double)maxMfn / chunkLength); loop++)
            {
                /*
                * Подготовка данных перед записью
                */

                string books = "";
                string authors = "";
                string bookAuthor = "";
                string keyword = "";
                string bookKeyword = "";
                string bookDelete = "";
                string bookBranch = "";
                int j = 0;
                int k = 0;
                int l = 0;

                for (int i = 0; i < chunkLength; i++)
                {
                    mfn++;
                    if (mfn <= maxMfn)
                    {
                        IrbisRecord record = client.ReadRecord(mfn);
                        if (record == null)
                        {
                            //Удаляем с бд книги с отсутствующими mfn
                            for (int m = mfn + 1; m < record.Mfn; m++)
                            {
                                bookDelete +=
                                    "book[" + l + "][category_id]=" + "8" + "&" +
                                    "book[" + l + "][mfn]=" + m + "&";
                                l++;
                            }
                        }
                        else
                        {
                            // Формируем чанк данных для переноса
                            books +=
                                "book[" + i + "][category_id]=" + "8" + "&" +
                                "book[" + i + "][mfn]=" + record.Mfn + "&" +
                                "book[" + i + "][name]=" + Title_PB(record) + "&" +
                                "book[" + i + "][year]=" + record.FM("210", 'd') + "&" +
                                "book[" + i + "][udk]=" + record.FM("675") + "&" +
                                "book[" + i + "][link]=" + record.FM("951", 'a') + "&" +
                                "book[" + i + "][link_name]=" + record.FM("951", 't') + "&";

                            if (record.FM("700", 'a') != null)
                            {
                                authors +=
                                    "author[" + j + "][surname]=" + record.FM("700", 'a') + "&" +
                                    "author[" + j + "][initials]=" + record.FM("700", 'b') + "&";
                                bookAuthor +=
                                    "attach[" + j + "][category_id]=" + "8" + "&" +
                                    "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + j + "][surname]=" + record.FM("700", 'a') + "&" +
                                    "attach[" + j + "][initials]=" + record.FM("700", 'b') + "&";
                                j++;
                            }
                            RecordField[] author_records = record.Fields.GetField("701").ToArray();
                            foreach (var author_record in author_records)
                            {
                                if (!((record.FM("700", 'a') == author_record.GetSubFieldText('a', 0)) &&
                                    (record.FM("700", 'b') == author_record.GetSubFieldText('b', 0))))
                                {
                                    authors +=
                                        "author[" + j + "][surname]=" + author_record.GetSubFieldText('a', 0) + "&" +
                                        "author[" + j + "][initials]=" + author_record.GetSubFieldText('b', 0) + "&";
                                    bookAuthor +=
                                        "attach[" + j + "][category_id]=" + "8" + "&" +
                                        "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                        "attach[" + j + "][surname]=" + author_record.GetSubFieldText('a', 0) + "&" +
                                        "attach[" + j + "][initials]=" + author_record.GetSubFieldText('b', 0) + "&";
                                    j++;
                                }
                            }
                            RecordField[] editor_records = record.Fields.GetField("702").ToArray();
                            foreach (var editor_record in editor_records)
                            {
                                authors +=
                                    "author[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "author[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                bookAuthor +=
                                    "attach[" + j + "][category_id]=" + "8" + "&" +
                                    "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "attach[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                j++;
                            }
                            RecordField[] keyword_records = record.Fields.GetField("610").ToArray();
                            foreach (var keyword_record in keyword_records)
                            {
                                keyword +=
                                    "keyword[" + k + "][value]=" + keyword_record.GetFieldText() + "&";
                                bookKeyword +=
                                    "attach[" + k + "][category_id]=" + "8" + "&" +
                                    "attach[" + k + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + k + "][value]=" + keyword_record.GetFieldText() + "&";
                                k++;
                            }
                            RecordField[] bookBranch_records = record.Fields.GetField("910").ToArray();
                            foreach (var bookBranch_record in bookBranch_records)
                            {
                                if (bookBranch_record.GetFirstSubFieldText('D') != null)
                                {
                                    bookBranch +=
                                    "attach[" + k + "][category_id]=" + "8" + "&" +
                                    "attach[" + k + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + k + "][branch_id]=" + Branch(bookBranch_record.GetFirstSubFieldText('D')) + "&";
                                    k++;
                                }
                            }
                        }

                    }
                }
                /*
                * Методы отправки данных в БД
                */
                MyWebRequest deleteBook = new MyWebRequest(domain + "catalog/book/delete", "POST", bookDelete);
                Console.WriteLine("deleteBook: " + deleteBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createBook = new MyWebRequest(domain + "catalog/book/createOrUpdate", "POST", books);
                Console.WriteLine("createBook: " + createBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createAuthor = new MyWebRequest(domain + "catalog/author/createOrUpdate", "POST", authors);
                Console.WriteLine("createAuthor: " + createAuthor.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookAuthor = new MyWebRequest(domain + "catalog/attach/bookAuthor", "POST", bookAuthor);
                Console.WriteLine("attachBookAuthor: " + attachBookAuthor.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createKeyword = new MyWebRequest(domain + "catalog/keyword/createOrUpdate", "POST", keyword);
                Console.WriteLine("createKeyword: " + createKeyword.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookKeyword = new MyWebRequest(domain + "catalog/attach/bookKeyword", "POST", bookKeyword);
                Console.WriteLine("attachBookKeyword: " + attachBookKeyword.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookBranch = new MyWebRequest(domain + "catalog/attach/bookBranch", "POST", bookBranch);
                Console.WriteLine("attachBookBranch: " + attachBookBranch.GetResponse());
                Console.WriteLine(new string('-', 60));
                Console.WriteLine(new string('-', 60));
            }
        }

        private static void ERI(ManagedClient64 client)
        {
            // Делаем переключение на базу ERI
            client.PushDatabase("PB");

            Console.WriteLine("PushDatabase ERI");

            /*
             * Разбитие данных на чанки, и отправка в БД
             */

            int maxMfn = client.GetMaxMfn() - 1;
            int chunkLength = 25;
            int mfn = 0;
            Console.WriteLine("Total records: " + maxMfn);

            for (int loop = 0; loop < (int)Math.Ceiling((double)maxMfn / chunkLength); loop++)
            {
                /*
                * Подготовка данных перед записью
                */

                string books = "";
                string authors = "";
                string bookAuthor = "";
                string keyword = "";
                string bookKeyword = "";
                string bookDelete = "";
                string bookBranch = "";
                int j = 0;
                int k = 0;
                int l = 0;

                for (int i = 0; i < chunkLength; i++)
                {
                    mfn++;
                    if (mfn <= maxMfn)
                    {
                        IrbisRecord record = client.ReadRecord(mfn);
                        if (record == null)
                        {
                            //Удаляем с бд книги с отсутствующими mfn
                            for (int m = mfn + 1; m < record.Mfn; m++)
                            {
                                bookDelete +=
                                    "book[" + l + "][category_id]=" + "8" + "&" +
                                    "book[" + l + "][mfn]=" + m + "&";
                                l++;
                            }
                        }
                        else
                        {
                            // Формируем чанк данных для переноса
                            books +=
                                "book[" + i + "][category_id]=" + "8" + "&" +
                                "book[" + i + "][mfn]=" + record.Mfn + "&" +
                                "book[" + i + "][name]=" + Title_PB(record) + "&" +
                                "book[" + i + "][year]=" + record.FM("210", 'd') + "&" +
                                "book[" + i + "][udk]=" + record.FM("675") + "&" +
                                "book[" + i + "][link]=" + record.FM("951", 'a') + "&" +
                                "book[" + i + "][link_name]=" + record.FM("951", 't') + "&";

                            if (record.FM("700", 'a') != null)
                            {
                                authors +=
                                    "author[" + j + "][surname]=" + record.FM("700", 'a') + "&" +
                                    "author[" + j + "][initials]=" + record.FM("700", 'b') + "&";
                                bookAuthor +=
                                    "attach[" + j + "][category_id]=" + "8" + "&" +
                                    "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + j + "][surname]=" + record.FM("700", 'a') + "&" +
                                    "attach[" + j + "][initials]=" + record.FM("700", 'b') + "&";
                                j++;
                            }
                            RecordField[] author_records = record.Fields.GetField("701").ToArray();
                            foreach (var author_record in author_records)
                            {
                                if (!((record.FM("700", 'a') == author_record.GetSubFieldText('a', 0)) &&
                                    (record.FM("700", 'b') == author_record.GetSubFieldText('b', 0))))
                                {
                                    authors +=
                                        "author[" + j + "][surname]=" + author_record.GetSubFieldText('a', 0) + "&" +
                                        "author[" + j + "][initials]=" + author_record.GetSubFieldText('b', 0) + "&";
                                    bookAuthor +=
                                        "attach[" + j + "][category_id]=" + "8" + "&" +
                                        "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                        "attach[" + j + "][surname]=" + author_record.GetSubFieldText('a', 0) + "&" +
                                        "attach[" + j + "][initials]=" + author_record.GetSubFieldText('b', 0) + "&";
                                    j++;
                                }
                            }
                            RecordField[] editor_records = record.Fields.GetField("702").ToArray();
                            foreach (var editor_record in editor_records)
                            {
                                authors +=
                                    "author[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "author[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                bookAuthor +=
                                    "attach[" + j + "][category_id]=" + "8" + "&" +
                                    "attach[" + j + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + j + "][surname]=" + editor_record.GetSubFieldText('a', 0) + "&" +
                                    "attach[" + j + "][initials]=" + editor_record.GetSubFieldText('b', 0) + "&";
                                j++;
                            }
                            RecordField[] keyword_records = record.Fields.GetField("610").ToArray();
                            foreach (var keyword_record in keyword_records)
                            {
                                keyword +=
                                    "keyword[" + k + "][value]=" + keyword_record.GetFieldText() + "&";
                                bookKeyword +=
                                    "attach[" + k + "][category_id]=" + "8" + "&" +
                                    "attach[" + k + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + k + "][value]=" + keyword_record.GetFieldText() + "&";
                                k++;
                            }
                            RecordField[] bookBranch_records = record.Fields.GetField("910").ToArray();
                            foreach (var bookBranch_record in bookBranch_records)
                            {
                                if (bookBranch_record.GetFirstSubFieldText('D') != null)
                                {
                                    bookBranch +=
                                    "attach[" + k + "][category_id]=" + "8" + "&" +
                                    "attach[" + k + "][mfn]=" + record.Mfn + "&" +
                                    "attach[" + k + "][branch_id]=" + Branch(bookBranch_record.GetFirstSubFieldText('D')) + "&";
                                    k++;
                                }
                            }
                        }

                    }
                }
                /*
                * Методы отправки данных в БД
                */
                MyWebRequest deleteBook = new MyWebRequest(domain + "catalog/book/delete", "POST", bookDelete);
                Console.WriteLine("deleteBook: " + deleteBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createBook = new MyWebRequest(domain + "catalog/book/createOrUpdate", "POST", books);
                Console.WriteLine("createBook: " + createBook.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createAuthor = new MyWebRequest(domain + "catalog/author/createOrUpdate", "POST", authors);
                Console.WriteLine("createAuthor: " + createAuthor.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookAuthor = new MyWebRequest(domain + "catalog/attach/bookAuthor", "POST", bookAuthor);
                Console.WriteLine("attachBookAuthor: " + attachBookAuthor.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest createKeyword = new MyWebRequest(domain + "catalog/keyword/createOrUpdate", "POST", keyword);
                Console.WriteLine("createKeyword: " + createKeyword.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookKeyword = new MyWebRequest(domain + "catalog/attach/bookKeyword", "POST", bookKeyword);
                Console.WriteLine("attachBookKeyword: " + attachBookKeyword.GetResponse());
                Console.WriteLine(new string('-', 60));

                MyWebRequest attachBookBranch = new MyWebRequest(domain + "catalog/attach/bookBranch", "POST", bookBranch);
                Console.WriteLine("attachBookBranch: " + attachBookBranch.GetResponse());
                Console.WriteLine(new string('-', 60));
                Console.WriteLine(new string('-', 60));
            }
        }

        private static void Main()
        {
            try
            {
                using (ManagedClient64 client = new ManagedClient64())
                {
                    //Чтения данных для подключения с файла
                    IniFile INI = new IniFile("config.ini");
                    string сonnectionString = "host=" + INI.ReadINI("IRBIS", "host") + ";port=" + INI.ReadINI("IRBIS", "port") +
                                              ";user=" + INI.ReadINI("IRBIS", "user") + ";password=" + INI.ReadINI("IRBIS", "password") + ";";
                    domain = INI.ReadINI("WEB", "domain");

                    //Подключение к ИРБИС
                    Console.WriteLine("Database connecting");
                    client.ParseConnectionString(сonnectionString);
                    client.Connect();
                    Console.WriteLine("Database connected");
                    Console.WriteLine(new string('-', 60));

                    //Выгрузка БД
                    RDR(client);
                    BOOKS(client);
                    MV(client);
                    DB2(client);
                    PVUD(client);
                    DB4(client);
                    RARIT(client);
                    PB(client);
                    ERI(client);

                    //Отключние от ИРБИС
                    Console.WriteLine("Irbris disconnect");
                    client.Disconnect();
                    Console.WriteLine("Irbris disconnected");
                    Console.WriteLine(new string('-', 60));                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
