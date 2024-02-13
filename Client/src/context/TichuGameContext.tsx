import { createContext, useContext, useState } from "react";
import { TichuGameState } from "../types";

type ContextType = {
    gameState: TichuGameState | undefined;
    setGameState: (gameState: TichuGameState | undefined) => void;
};

const TichuGameContext = createContext<ContextType>({
    gameState: undefined,
    setGameState() {}
});

export const TichuGameProvider = (props: React.PropsWithChildren) => {
    const { children } = props;
    const [gameState, setGameState] = useState<TichuGameState | undefined>(undefined);
    return <TichuGameContext.Provider value={{
        gameState: gameState,
        setGameState: setGameState
    }}>
    {children}</TichuGameContext.Provider>;
}

export const useTichuContext = () => {
    const context = useContext(TichuGameContext);

    if (context === undefined){
        throw new Error("useTichuContext must be used within a TichuGameProvider")
    }

    return context;
}