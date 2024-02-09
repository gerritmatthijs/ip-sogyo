namespace Tichu

type StatusText = 
    | NoText
    | Alert of text: string
    | Message of text: string