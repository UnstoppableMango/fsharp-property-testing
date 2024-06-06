module AdditionTests

open FsCheck.Xunit

[<Property>]
let ``Commutative`` a b =
    a + b = b + a

[<Property>]
let ``Associative`` a b c =
    (a + b) + c = a + (b + c)

[<Property>]
let ``Identity`` a =
    0 + a = a

[<Property>]
let ``Inverse`` a =
    a + -a = 0
