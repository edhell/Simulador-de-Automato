# Simulador-de-Automato
Simulador de Autômato<br>
Trabalho de Teoria da Computação<br>
Simula o teste de dois programas.<br>
Mais em www.edinfo.com.br<br>

Entrada -> Programa Monolítico com Instruções Rotuladas Simples<br>
* Entradas:<br>
1: faça F vá_para 2<br>
2: se T1 então vá_para 1 senão vá_para 3 <br>
3...8 (varias linhas, resumindo)<br>
9: se T4 então vá_para 1 senão vá_para 10<br>

* Gera:<br>
1: (A,2),(A,2)<br>
2: (A,2),(B,4)<br>
3...8 (varias linhas, resumindo)<br>
9: (A,2),(Parada, ε)<br>

* Resultado:<br>
A0 = {ε}<br>
A1 = {ε,2,1}<br>
