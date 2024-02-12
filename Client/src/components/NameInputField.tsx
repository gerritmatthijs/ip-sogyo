import { ChangeEventHandler } from "react";

type Props = {
    id: number
    onChange: ChangeEventHandler<HTMLInputElement>
    value: string
}

export default function NameInputField(props: Props) {
    const { id, onChange, value } = props;
    return (
        <div>
            <label>Name of Player {id}: </label>
            <input type="text" onChange={onChange} value={value}/>
        </div>
    )
}