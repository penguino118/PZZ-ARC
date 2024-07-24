# PZZ-ARC
### A BrawlBox inspired modding tool for .pzz packages in [GioGio's Bizarre Adventure (PS2)](https://jojowiki.com/GioGio%27s_Bizarre_Adventure)
[pzzcompressor_jojo](https://github.com/infval/pzzcompressor_jojo) originaly written by [infval](https://github.com/infval)<br/>

You can inspect the contents of any valid .pzz file and modify the content  inside. The following data types are detected:
* **.amo** - Artistoon Model Data
* **.ahi** - Skeleton Data
* **.txb** - Texture Data
* **.aan** - Artistoon Animation Data
* **.sdt** - Shadow Mesh Data
* **.hit** - Player Collision Data
* **.hits** - Stage Collision Data
* **.txt** - Text Data

## AFSLib
To load .pzz files from AFS archives, [AFSLib](https://github.com/MaikelChan/AFSLib/tree/main) is used, which is licensed under the [MIT license](https://github.com/penguino118/PZZ-ARC/Tools/AFSLib/blob/main/AFSLib/LICENSE).