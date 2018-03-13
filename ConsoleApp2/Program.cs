using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace ConsoleApp2
{
    class Program
    {
        const string StorageAccountName = "johnny644";
        const string StorageAccountKey = "fdqNsSn20zoE3DAzseRFjyOMuQ/Vtm6M7ZHzUQP1Oqvp39b8utZPcZe3i7mm8x557bXUhHkIsKlz9E+kaZiphg==";

        static void Main(string[] args)
        {
            var storageAccount = new CloudStorageAccount(new StorageCredentials(StorageAccountName, StorageAccountKey), true);
            var tableClient = storageAccount.CreateCloudTableClient();
            var usersTable = tableClient.GetTableReference("users");
            usersTable.CreateIfNotExists();

            usersTable.Execute(TableOperation.Insert(new UsersEntity("Jonny Johnson", "jonnick")));

                Console.WriteLine("Завершено...");
                Console.ReadLine();
        }
    }

    public class UsersEntity : TableEntity
    {
        public UsersEntity() { }
        public UsersEntity(string name, string userName)
        {
            PartitionKey = name;
            RowKey = userName;
        }
        public string Name => PartitionKey;
        public string UserName => RowKey;

        public override string ToString() => $"{Name} {UserName}";
    }
}
