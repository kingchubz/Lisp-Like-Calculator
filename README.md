# Lisp Like Calculator

![Image1](/Lisp_Interpreter/img/llc1.png)

This is an incomplete Scheme (a dialect of Lisp) interpreter. Without any system IO it can be used as fancy, but inconvenient, calculator.

## Implemented Features

Lisp-Like-Calculator uses polish notation and parenthesis to invoke functions `(<operator> <arg1> <arg2> ...)`.

### Arithmetic operations `+ - * /`

Take arbitrary amount of arguments and perform operation on the first argument.

Code examples:

`(
 + 
 10 
 (- (* 10 (/ 2 2)))
) #out: 0`

### Rounding `round`

Takes one number and returns a new number, rounded towards closest whole number.

`(round (/ 3 2)) #out: 2`

### Comparison predicates `= > < >= <=`

Take two arguments and return either '#t' for true or '#f' for false statements.

Code examples:

`(= 3 4) #out: #f`

`(> 3 4) #out: #f`

`(< 3 4) #out: #t`

`(>= 4 4) #out: #t`

`(<= 4 4) #out: #t`

### Type predicates `empty? constant? symbol? number? list? procedure?`

Take one argument and compare its type, returning either '#t' or '#f'.

_`empty?` doesn't work correctly at the moment._

Code examples:

`(constant? #f) #out: #t`

`(symbol? ( car (quote a b))) #out: #t`

`(number? 0) #out: #t`

`(list? (cons 1 4)) #out: #t`

`(define func (lambda (x) (+ x 1)))`
`(procedure? func) #out: #t`

### Equality predicate `eq?`

_Doesn't work correctly at the moment._

### Conditional statements `if`

Structure that takes three arguments `(if <predicate> <true-expression> <false-expression>)`

Code examples:

`(define x 1)`

`(if (> x 2) 11 22) #out:22`

### Construction of pairs `cons`

Takes two arguments and returns them as a pair.

Code examples:

`(cons 1 (cons 2 3)) #out: (1 ( 2 3))`

### Pair accessors `car cdr`

Takes pair as an argument and returns either left part `car` or right part `cdr` of pair.

Code examples:

`(car (cons 1 (cons 2 3)) #out: 1`

`(cdr (cons 3 2)) #out: ( 2 3)`

### List `quote`

Takes arbitrary amount of arguments and returns them as list.

Code examples:

`(cdr (quote a b c d)) #out: ( b c d)`

### Consecutive evaluation `begin`

Takes arbitrary amount of statements and executes them, returning value of the last statement.

Code examples:
  
`(begin (define x 10) (define y 20) (/ x y)) #out: 0.5`

### Variable definition `define`

Assigns _value_ to a _name_. _Value_ could be `lambda` expression.

Code examples:

`(define x 11)`

`(- x 6) #out: 5`

### Variable mutation `set!`

Modifies __already__ existing variable, could access global variable from function body.

Code examples:

`(define x 11)`

`(set! x (+ x 1))`

`x #out:12`

### Lambda expression `lambda`

Creates anonymous function which takes arbitrary list of arguments and an expression `(lambda (<arg1> <arg2> ...) <expression>)`

Code examples:

`((lambda (x) (+ x 1)) 1) #out: 2`

## Example: factorial function

`(define fact 
	(lambda (n) 
		(* n (if (<= n 1) 1 (fact (- n 1))))
	)
)`

`(fact 6) #out:720`
