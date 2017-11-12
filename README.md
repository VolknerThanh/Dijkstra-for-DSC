# Dijkstra-for-DSC

## Upload lần 1

> đọc file từ danh sách cạnh 

> lưu vào ma trận kề

> lưu vào danh sách kề sau khi đọc từ ma trận kề

```csharp
string[] s = sr.ReadLine().Trim().Split(' ');

int d = int.Parse(s[0]) - 1;
int c = int.Parse(s[1]) - 1;
int ts = int.Parse(s[2]);

matrix[d, c] = matrix[c, d] = ts;
```

Bước này dùng để lưu giá trị của 1 hàng trong danh sách cạnh vào ma trận kề

#### Hàm chuyển đổi

```csharp
dsk = new LinkedList<Tuple<int, int>>[n];
for (int i = 0; i < n; i++)
{
    dsk[i] = new LinkedList<Tuple<int, int>>();
    for (int j = 0; j < n; j++)
    {
        if (matrix[i, j] != -1)
            dsk[i].AddLast(new Tuple<int, int>(j + 1, matrix[i, j]));
    }
}
```

___

## Upload lần 2

> đọc file từ danh sách cạnh nhưng không lưu lại

> thay vào đó là lưu trực tiếp vào danh sách kề

> bước này bỏ qua giai đoạn trung gian là ma trận kề

```csharp
int dem = 1;
bool[] kiemtra = new bool[n];
dsk = new LinkedList<Tuple<int, int>>[n];
for (int i = 0; i < soCanh; i++)
{
    string[] s = sr.ReadLine().Trim().Split(' ');
    int d = int.Parse(s[0]) - 1;
    int c = int.Parse(s[1]) - 1;
    int ts = int.Parse(s[2]);
    if (dem != d + 1)
        dem = d + 1;
    if(dem == d + 1)
    {
        if(kiemtra[d] == false)
        {
            dsk[d] = new LinkedList<Tuple<int, int>>();
            kiemtra[d] = true;
        }
        dsk[d].AddLast(new Tuple<int, int>(c + 1, ts));
        if(kiemtra[c] == false)
        {
            dsk[c] = new LinkedList<Tuple<int, int>>();
            kiemtra[c] = true;
        }
        dsk[c].AddLast(new Tuple<int, int>(d + 1, ts));
    }
}

```

> Biến `dem` dùng để gán vị trí của danh sách : `dsk[d]`

> Mảng `kiemtra` để kiểm tra số đó đã tồn tại chưa, để tránh tái khai báo `dsk[...] = new LinkedList<Tuple<int, int>>()`

_**Cách giải này có tác dụng:**_

```
1 2 3
1 3 18
1 4 6
1 5 9
1 6 14
2 3 12
2 4 2
3 4 7
3 7 4
4 5 3
4 6 6
5 6 4
6 7 2
```

đỉnh 1 nối với 2, 3, 4, 5, 6

nếu như vậy thì 2, 3, 4, 5, 6 cũng nối với 1

ta thấy rằng đây là danh sách cạnh, mỗi cạnh chỉ xuất hiện 1 lần, nhưng đối với danh sách kề, số cạnh nối với 2 đỉnh bằng 2

tức là đỉnh 1 nối đỉnh 2 cạnh 1-2, ngược lại đỉnh 2 nối đỉnh 1 có cạnh là 1-2 hoặc 2-1.

```csharp
dsk[d].AddLast(new Tuple<int, int>(c + 1, ts));

dsk[c].AddLast(new Tuple<int, int>(d + 1, ts));
```