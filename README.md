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

===

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

