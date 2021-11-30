# LC_Encoding
比特bit、字节byte-》Ascii、Default、Unicode、BigEndianUnicode、UTF7、UTF8、UTF32编码关系和转换讲解

## "1个字节是8个比特，1个字母是1个字节，1个汉字是2个字节"
- 上面这句话很多人都看过听过，但大部分人都不知道为什么 (例如前两天的我)，这句话在某些规定下其实是不正确的

## bit
- 中文：比特
- 表示0或1
- 计算机中最小的单位
## byte
- 中文：字节
- 1byte==8bit 表示8个比特(为什么不是2个或者4个 后面解答)
- 插一句 比特币 字节跳动 名字取的真好

## ASCII码
- 美国制定的一套让计算机识别、保存、读取字符的标准
- 字符定义： 数字'7' 字母'a' 操作字符（删除 确定）
- 一共有128个字符 每个字符都有唯一的编码 查看ascii.pdf
- 计算机中最小的单位是比特所以我们用比特来存储
- 那么需要多少个比特来存一个字符呢？
- 2比特 最多保存 4个数据 00 01 10 11
- 4比特 最多保存16个数据 0000 0001 0010 0011 ...... 1111
- 7比特 最多保存128个数据 0000000 0000001 0000010 0000011 ...... 1111111
- 8比特 最多保存255个数据 00000000 00000001 00000010 00000011 ...... 11111111
- 其实7bit就可以保存128个字符 但是最后还是用8比特 因为当时用第八位来作为奇偶校验位（奇偶校验位现在没用）
- byte的概念最早是用来表示一个"字" 也就是char 最后通俗约定 8bit表示1byte
- 例外：GSM默认采用7bit编码
