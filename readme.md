# Prova BonifiQ Backend
Olá! Essa prova foi entregue.

## Trabalho

### Parte1Controller
Esse controller foi criado para gerar uma API que sempre retorna um número aleatório. 
Você pode vê-lo funcionando ao rodar o projeto e na página do Swagger, clique em Parte 1 > Try it Out > Execute.

Esse código, no entanto, tem algum problema: ele sempre retorna o mesmo valor.
Seu trabalho, portanto, é corrigir esse comportamente: cada vez que a chamada é realizada um número diferente deverá ser retornado.

Outro problema a ser corrigido: o número salvo no banco deve ser único. Muitas vezes uma exception é gerada quando se tenta salvar o mesmo número mais de uma vez. Faça também a correção desse problema.

### Parte2Controller
Essa API deveria retornar os produtos cadastrados de forma paginada. O usuário informa a página (page) desejada e o sistema retorna os 10 itens da mesma.
O problema é que não importa qual número de página é utilizado: os resultados estão vindo sempre os mesmos. E não apenas os 10.

Você precisa portanto:
1. Corrigir o bug de paginação. Ao passar page=1, deveria ser retornado os 10 (0 a 9) primeiros itens do banco. Ao passar page=2 deveria ser retornado os itens subsequentes (10 a 19), etc
2. Note que na Action do Controller, chamamos o ```ProductService```. Fazemos isso, instanciando o mesmo (```var productService = new ProductService(_ctx);```). Essa é uma prática ruim. Altere o código para que utilize Injeção de Dependência.
3. Agora, explore os arquivos ```/Models/CustomerList``` e ```/Models/ProductList```. Eles são bem parecidos. De fato, deve haver uma forma melhor de criar esses objetos, com menos repetição de código. Faça essa alteração.
4. Da mesma forma, como você melhoraria o ```CustomerService```e o ```ProductService``` para evitar repetição de código?

### Parte3Controller
Essa API cria o pagamento de uma compra (```PlaceOrder```). Verifique o método ```PayOrder``` da classe ```OrderService```.
Você deve ter percebido que existem diversas formas de pagamento (Pix, cartão de crédito, paypal), certo?
Essa classe, no entanto, é problemática. Imagine que teríamos que incluir um novo método de pagamento, seria mais um ```if```na estrutura.

Você precisa:
1. Faça uma alteração na arquitetura para que fique mais bem estruturado e preparado para o futuro.
Tenha certeza que o princípio Open-Closed será respeitado.
2. O retorno do método é um objeto `Order`, que possui um campo `OrderDate`. Tenha certeza que ao salvar o pedido no banco o campo `OrderDate` é salvo como UTC. Entretanto, o retorno do Controller deve exibir o `OrderDate` em fuso horário brasileiro (UTC-3)

### Parte4Controller
Essa API faz uma validação de negócio e retorna se o consumidor pode realizar uma compra.
Verifique o arquivo ```CanPurchase``` da classe ```CustomerService``` e note que ele aplica diversas regras de negócio.

Seu trabalho aqui será:
1. Crie testes unitários para este método. Tente obter o máximo de cobertura possível. Se precisar, pode rearquitetar o código para facilitar nos testes.

Você pode utilizar qualquer framework de testes que desejar. 
