`timescale 1ns / 1ps
module borrador(

    );
    
wire [11:0]SControl_ref1,SControl_ref2,SControl_ref3;
reg EnCtrolref=0;
Alg_ctrl_Ref Algref01(.sysclk(sysclk),.sync(sync),.En(EnCtrolref),.I_Actual(I_Actual),
.ref(ref),.pasos(pasos),.SControl1(SControl_ref1),.SControl2(SControl_ref2)
,.SControl3(SControl_ref3),.CtrlOk(CtrlOkref),.En_PM(En_PM));

endmodule