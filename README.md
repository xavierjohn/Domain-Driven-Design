# Domain-Driven-Design
Domain Driven Design base types.

Create value type by deriving your class from `ValueObject<T>`
Example: 
`public class Currency : ValueObject<Currency>`

ValueOject will implement the Equals and GetHashCode for the derived class by comparing all the properties.

Create Entity type by deriving the class from `Entity<T>` which will compare objects by the Key.
