`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer:    Jaime Cariñe 
// 
// Create Date:    21:48:19 07/05/2015 
// Design Name:    Porg UART Rx
// Module Name:    UART_Rx 
//////////////////////////////////////////////////////////////////////////////////
module UART_Rx(
input clk_100Mhz,Tick,
input 		RsRx,	// ticks y pin físico de entrada
output reg  RxOk,		//bandera de recepción completa
output reg 	[7:0] BufferRx	//dato recepcionado 
);

///////////////////////////////////////////////  /////////////////////////////////////
//valores para entrar a los sucesivos bloques

//Registros locales
reg [1:0] regBloqueRx; 						// valor de entrada a cada bloque
reg [2:0] regCountBRx; 						//cuenta 8 Bit Recibidos
reg [3:0] regCountTick=0;					//cuenta 16 estados, necesita solo 4 bit
reg [7:0] regBufBitRx=0;					//almacena los bits validos
reg [4:0] regMuestreo=0;					//muestrea la se;al Rx



always @(posedge clk_100Mhz)begin

if(Tick)begin

	 case(regBloqueRx[1:0])
		
		0: begin	
		//espera el inicio del primer bit, éste es un cero lógico.
			if(~RsRx)begin 
				regBloqueRx[1:0]<=2'd1;
				regCountTick[3:0]<=4'd0;
				
			end
		end
		1:	begin	
			 //bit de inicio 16tics en cero.
					if(&regCountTick[3:0])begin 
					regBloqueRx[1:0]<=2'd2;
					regMuestreo[4:0]<=5'd0;
					regCountBRx[2:0]<=3'd0; //cuenta los 8 bits entrante.
					RxOk 	<= 1'b0; //desactiva recepción Ok
					end
					regCountTick[3:0]<=regCountTick[3:0] + 4'd1;
		end
			
		2:	begin	
		//muestrea la señal entrante, sumandola al registro regUnbitDato por cadad nivel alto..
			regMuestreo[4:0]<=regMuestreo[4:0]+{4'd0,RsRx};
			
				if(&regCountTick[3:0])begin		// cuando regCountTick==4b1111;
		
		//si el bufer muestreo recepciono más de 7 niveles altos entonces carga un 1;
		// de lo contrario carga un 0
						if(regMuestreo[4:0]>7)
						regBufBitRx[regCountBRx]<=1'b1; 
						else
						regBufBitRx[regCountBRx]<=1'b0; 
						
		// cuando regCountBRx==3b111; pasa al sigiente ciclo, ya que son 8bits.
						if(&regCountBRx[2:0]) 	
						regBloqueRx[1:0]<=2'd3;
						else
						regCountBRx[2:0]<=regCountBRx[2:0]+3'b1;
						
						regMuestreo[4:0]<=5'd0;
				end
				regCountTick[3:0]<=regCountTick[3:0] + 4'd1;
		end
		
		3:		begin
				regCountTick[3:0]<=regCountTick[3:0] + 4'd1; //16 ticks esperando el bit de parada
				//guarda el dato recepcionado.
				BufferRx[7:0]<=regBufBitRx[7:0]; 
				if(regCountTick[3:0]>4'd4)begin //0.25 bit de parada
				regBloqueRx[1:0]<=2'd0;
				RxOk 	<= 1'b1; //avisa recepción Ok
				end
		
		end	
	 endcase
	end
end

endmodule
