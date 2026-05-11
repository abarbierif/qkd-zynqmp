# Prototipo

Este documento contiene información sobre los componentes opto-electrónicos para montar un prototipo en fibra _single-mode_ (de un solo núcleo) del sistema completo de _Quantum Key Distribution_ (QKD) basado en fibra multinúcleo con 4 núcleos (4MCF).

- [View Prototype Setup PDF](../figs/prototype_setup.drawio.pdf)
- [View Full Setup PDF](../figs/full_setup.pdf)

![Protype Setup SVG](../figs/prototype_setup.svg)

## Componentes

### Optical

**Laser**: 
- Discutir con profe Gabriel
- [https://www.thorlabs.com/fiber-coupled-laser-sources](https://www.thorlabs.com/fiber-coupled-laser-sources)

**Intensity Modulator**:
- Discutir con profe Gabriel
- [https://www.thorlabs.com/lithium-niobate-electro-optic-modulators-fiber-coupled-1260-nm---1625-nm?tabName=Overview](https://www.thorlabs.com/lithium-niobate-electro-optic-modulators-fiber-coupled-1260-nm---1625-nm?tabName=Overview)

**Phase Modulator**: LN65S-FC
- 40 GHz Phase Modulator, FC/PC Connectors, 1260 nm - 1625 nm, Small Form Factor Housing
- [https://www.thorlabs.com/item/LN65S-FC?aID=b30c756eccc2c2d01e77128fdc1ce23a&aC=2](https://www.thorlabs.com/item/LN65S-FC?aID=b30c756eccc2c2d01e77128fdc1ce23a&aC=2)
- [Spec Sheet](https://media.thorlabs.com/globalassets/items/l/ln/ln6/ln65s-fc/20866-s01.pdf?v=0116125331)
- Price: $3,390.83

**Detectors**:
- Discutir con profe Gabriel
- [https://www.thorlabs.com/detectors](https://www.thorlabs.com/detectors)

### Electrical

**SoC Evaluation Board**: AMD Zynq UltraScale+™ MPSoC ZCU102 Evaluation Kit
- Part number: EK-U1-ZCU102-G
- [https://www.amd.com/en/products/adaptive-socs-and-fpgas/evaluation-boards/ek-u1-zcu102-g.html#tabs-e50ad92206-item-f5d1f6b305-tab](https://www.amd.com/en/products/adaptive-socs-and-fpgas/evaluation-boards/ek-u1-zcu102-g.html#tabs-e50ad92206-item-f5d1f6b305-tab)
- [ZCU102 User Guide](https://docs.amd.com/v/u/en-US/ug1182-zcu102-eval-bd)
- **Note**: Has two FMC High Pin Count (HPC) connectors
- Price: $3,234.00

**Analog to Digital Converter**:
- Discutir con Javier
- AD-FMCDAQ2-EBZ? If so [https://www.analog.com/en/resources/evaluation-hardware-and-software/evaluation-boards-kits/eval-ad-fmcdaq2-ebz.html#eb-overview](https://www.analog.com/en/resources/evaluation-hardware-and-software/evaluation-boards-kits/eval-ad-fmcdaq2-ebz.html#eb-overview)
- Price: $2,023.56

**Digital to Analog Converter**:
- Discutir con Javier

**Phase Modulator's Driver**: DR-AN-10-MO
- Analog RF Driver, 11 GHz bandwidth, Linear amplifier
- [https://www.exail.com/product/analog-drivers-photonics](https://www.exail.com/product/analog-drivers-photonics)
- [Datasheet](https://www.exail.com/media-file/7722/exail-datasheet-dr-an-10-mo-rf-modulator-driver.pdf)
- [Operating manual](https://www.exail.com/media-file/9580/exail-driver-operating-manual.pdf)
- Price: 1920 €

## Cables
**Detectors -> ADC -> ZCU102**:
- |Fiber-BNC| <- cable -> |SMA(F)-FMC|

  - SMA-to-BNC: [https://www.thorlabs.com/item/CA2824](https://www.thorlabs.com/item/CA2824)
      - Price: $20.47

 **ZCU102 -> DAC -> Driver -> PM**:
- |FMC-SMA(F)| <- cable -> |V(F)-V(M)| <- cable -> |SMP(GPO)(M)|

  - SMA-to-SMA: [https://www.thorlabs.com/item/CA2924](https://www.thorlabs.com/item/CA2924)
      - Price: $19.83
  - SMA-to-V(1.85mm) adapter: [https://www.digikey.com/en/products/detail/fairview-microwave/FMAD1202/22222974](https://www.digikey.com/en/products/detail/fairview-microwave/FMAD1202/22222974)
      - Price: $313.39
  - V(F)-SMP(GPO)(F) adapter: [https://www.digikey.com/en/products/detail/amphenol-sv-microwave/1112-6218/25600244?s=N4IgTCBcDaIIwLAWgGxjgDhAXQL5A](https://www.digikey.com/en/products/detail/amphenol-sv-microwave/1112-6218/25600244?s=N4IgTCBcDaIIwLAWgGxjgDhAXQL5A)
      - Price: $216.20

## Some links
- [JESD204B Survival Guide](https://www.analog.com/media/en/technical-documentation/technical-articles/JESD204B-Survival-Guide.pdf)

