namespace SInterpreter
{
    public enum Tokens
    {
        STRING,
        INTEGER,
        NUMBER,      
        IDENTIFIER,  

        PLUS,        
        MINUS,        
        MULTIPLY,     
        DIVIDE,      

        ASSIGN,       

        EQUAL,        
        NOT_EQUAL,   
        LESS_THAN,   
        GREATER_THAN, 
        LESS_EQUAL,  
        GREATER_EQUAL,
        INCREMENT,    
        DECREMENT,    
        MINUS_EQUALS, 
        PLUS_EQUALS,

        LPAREN,      
        RPAREN,       
        LBRACE,      
        RBRACE,       
        SEMICOLON,  
        COMMA,      
        QUOTATIONMARK,

        IF,         
        ELSEIF,    
        ELSE,        
        FOR,        

        PRINT,
        POW,
        SQRT,
        SIN,
        COS,
        RAN,
        LENGTH,
        LOG,
        ABS,
        MAX,
        MIN,
        TAN,
        ASIN,
        ACOS,
        ATAN,

        UNKNOWN       
    }
}
