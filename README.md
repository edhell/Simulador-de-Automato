# Simulador-de-Automato
Simulador de Autômato
Trabalho de Teoria da Computação
Simula o teste de dois programas .

Entrada -> Programa Monolítico com Instruções Rotuladas Simples
* Entradas:
1: faça F vá_para 2<br>
2: se T1 então vá_para 1 senão vá_para 3 
3...8 (varias linhas, resumindo)
9: se T4 então vá_para 1 senão vá_para 10

* Gera:
1: (A,2),(A,2)
2: (A,2),(B,4)...
3...8 (varias linhas, resumindo)
9: (A,2),(Parada, ε)

* Resultado:
A0 = {ε}
A1 = {ε,2,1}
