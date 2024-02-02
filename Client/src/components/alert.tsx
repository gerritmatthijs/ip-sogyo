type Props = {
    text: string;
    onClick: () => void;
}

export const Alert = (props: Props) => {
    const { text, onClick } = props;
    return <div className="alert" onClick={() => onClick()}> {text} <br/> </div>
}