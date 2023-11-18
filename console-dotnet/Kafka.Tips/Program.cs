using Confluent.Kafka;

#region Consumindo mensagem no Kafka mais de uma vez, quando a mesma ainda não foi commitada.
/*
const string Topico = "desenvolvedor.io";
var i = 1;

_ = Task.Run(() => Consumir("grupo4", AutoOffsetReset.Latest));

while (true)
{
    Console.ReadLine();
    Produzir(i).GetAwaiter().GetResult();
    i++;
}

static async Task Produzir(int i)
{
    var configuracao = new ProducerConfig
    {
        Acks = Acks.All,
        BootstrapServers = "localhost:9092"
    };
    var key = Guid.NewGuid().ToString();
    var mensagem = $"Mensagem ( {i} ) KEY: {key}";

    Console.WriteLine(">> Enviada:\t " + mensagem);

    try
    {
        using var producer = new ProducerBuilder<string, string>(configuracao).Build();

        var result = await producer.ProduceAsync(Topico, new Message<string, string>
        {
            Value = mensagem
        });

        await Task.CompletedTask;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex.ToString());
    }
}

static void Consumir(string grupoId, AutoOffsetReset autoOffsetReset)
{
    var clientId = Guid.NewGuid().ToString()[0..5];

    var conf = new ConsumerConfig
    {
        ClientId = clientId,
        GroupId = grupoId,
        BootstrapServers = "localhost:9092",
        AutoOffsetReset = autoOffsetReset,
        EnablePartitionEof = true,
        EnableAutoCommit = false,
        EnableAutoOffsetStore = false //offset não vai ser deslocada automaticamente
    };

    using var consumer = new ConsumerBuilder<string, string>(conf).Build();

    consumer.Subscribe(Topico);

    int Tentativas = 0;

    while (true)
    {
        var result = consumer.Consume();

        if (result.IsPartitionEOF)
            continue;

        var message = "<< Recebida: \t" + result.Message.Value;
        Console.WriteLine(message);

        //Tentar processar mesagem
        Tentativas++;

        if(!ProcessarMensagem(result) && Tentativas < 3)
        {
            consumer.Seek(result.TopicPartitionOffset); //move o ponteiro offset do kafka, para essa mensagem ser lida novamente no próximo consumo.
            continue;
        }

        if(Tentativas > 1)
        {
            //Publicar mensagem em uma fila para analise!
            Console.WriteLine("Enviando mensagem para: DeadLetter");
            Tentativas = 0;
        }

        consumer.Commit(result);
    }
}

static bool ProcessarMensagem(ConsumeResult<string, string> result)
{
    Console.WriteLine($"KEY: {result.Message.Key} - {DateTime.Now}");
    Task.Delay(2000).Wait();
    return false;
}
*/
#endregion

#region  EXEMPLO TIPO DE CONSUMO (AutoOffReset: Earliest, Latest)
/*
const string Topico = "desenvolvedor.io";
var i = 1;

for (i = 1; i <= 5; i++)
{
   // await Produzir(i);
}

_ = Task.Run(() => Consumir("grupo1", AutoOffsetReset.Earliest));
_ = Task.Run(() => Consumir("grupo2", AutoOffsetReset.Earliest));
_ = Task.Run(() => Consumir("grupo3", AutoOffsetReset.Earliest));
_ = Task.Run(() => Consumir("grupo4", AutoOffsetReset.Latest));

while (true)
{
    Console.ReadLine();
    Produzir(i).GetAwaiter().GetResult();
    i++;
}

static async Task Produzir(int i)
{
    var configuracao = new ProducerConfig
    {
        Acks = Acks.All,
        BootstrapServers = "localhost:9092"
    };

    var mensagem = $"Mensagem ( {i} ) - *{Guid.NewGuid()} - {DateTime.Now}*";

    Console.WriteLine(">> Enviada:\t " + mensagem);

    try
    {
        using var producer = new ProducerBuilder<Null, string>(configuracao).Build();

        var result = await producer.ProduceAsync(Topico, new Message<Null, string>
        {
            Value = mensagem
        });

        await Task.CompletedTask;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex.ToString());
    }
}

static void Consumir(string grupoId, AutoOffsetReset autoOffsetReset)
{
    var clientId = Guid.NewGuid().ToString()[0..5];

    var conf = new ConsumerConfig 
    {
        ClientId = clientId,
        GroupId = grupoId,
        BootstrapServers = "localhost:9092",
        AutoOffsetReset = autoOffsetReset,
        EnablePartitionEof = true,
        EnableAutoCommit = false
    };

    using var consumer = new ConsumerBuilder<Null, string>(conf).Build();

    consumer.Subscribe(Topico);

    while(true)
    {
        var result = consumer.Consume();
        if(result.IsPartitionEOF)
        {
            continue;
        }

        var message = "<< Recebida \t" + result.Message.Value + $" - {grupoId}-{autoOffsetReset}-{clientId}";
        
        Console.WriteLine(message);

        consumer.Commit(result);
    }
}*/

#endregion

#region APLICANDO EXEMPLOS DE TIPOS DE ACKS: None, Leader, All.
/*
const string Topico = "desenvolvedor.io";

var configuracao = new ProducerConfig
{
    Acks = Acks.All,
    BootstrapServers = "localhost:9092"
};

var mensagem = $"Mensagem {Guid.NewGuid()}";

try
{
    using var producer = new ProducerBuilder<Null, string>(configuracao).Build();
    
    var result = await producer.ProduceAsync(Topico, new Message<Null, string> 
    {
        Value = mensagem
    });
    //colocar break point aqui para ver os tipos de acks no status
}
catch(Exception ex)
{
    Console.Error.WriteLine(ex.ToString());
}
*/
#endregion