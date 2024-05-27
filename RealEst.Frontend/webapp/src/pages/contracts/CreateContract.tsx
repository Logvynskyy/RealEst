import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../../../@/components/ui/select";
import { Endpoints } from "../../constants/endpoints";
import { Unit } from "../../interfaces/Unit";
import { Tennant } from "../../interfaces/Tennant";

const CreateContract = () => {
  const [units, setUnits] = useState<Unit[]>();
  const [tennants, setTennants] = useState<Tennant[]>();
  const [unitId, setUnitId] = useState<number>(0);
  const [tennantId, setTennantId] = useState<number>(0);
  const navigate = useNavigate();
  const nameRef = useRef<HTMLInputElement>(null);
  const ibanRef = useRef<HTMLInputElement>(null);
  const priceRef = useRef<HTMLInputElement>(null);
  const rentFromRef = useRef<HTMLInputElement>(null);
  const rentToRef = useRef<HTMLInputElement>(null);

  const getUnits = async () => {
    try {
      const response = await fetch("https://localhost:7115/api/Units", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("JWTToken"),
        },
      });

      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      const responseData = await response.json();
      console.log(responseData);

      setUnits(responseData);
    } catch (error) {
      console.error("Error getting units:", error);
      return [];
    }
  };

  const getTennants = async () => {
    try {
      const response = await fetch("https://localhost:7115/api/Tennants", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("JWTToken"),
        },
      });

      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      const responseData = await response.json();
      console.log(responseData);

      setTennants(responseData);
    } catch (error) {
      console.error("Error getting tennants:", error);
      return [];
    }
  };

  useEffect(() => {
    getUnits();
    getTennants();
  }, []);

  const handleSubmit = async () => {
    try {
      const name = nameRef.current?.value;
      const iban = ibanRef.current?.value;
      const price = priceRef.current?.value;
      const rentFrom = rentFromRef.current?.value;
      const rentTo = rentToRef.current?.value;

      const response = await fetch(
        "https://localhost:7115/api/Contracts/create",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + localStorage.getItem("JWTToken"),
          },
          body: JSON.stringify({
            id: 0,
            name,
            unitId,
            iban,
            tennantId,
            price,
            rentFrom,
            rentTo,
          }),
        }
      );

      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      try {
        navigate(Endpoints.contracts);
      } catch (error) {
        console.error("Redirect error:", error);
      }
    } catch (error) {
      console.error("Creating error:", error);
    }
  };

  return (
    <div className="w-full flex min-h-screen justify-center items-center bg-[#6DDCFF]">
      <div className="bg-white p-8 rounded-lg shadow-md w-[30rem]">
        <form className="flex flex-col gap-4">
          <h1 className="text-2xl">Назва</h1>
          <input
            type="text"
            name="name"
            placeholder="Enter name of the Contract"
            required
            ref={nameRef}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Об'єкт</h1>
          <Select onValueChange={(value) => setUnitId(Number.parseInt(value))}>
            <SelectTrigger className="w-full">
              <SelectValue placeholder={"Об'єкт"} />
            </SelectTrigger>
            <SelectContent>
              {units?.length && units.length > 0 && units.map((unit, index) => (
                <SelectItem key={index} value={unit.id.toString()}>
                  {unit.displayString}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
          <h1 className="text-2xl">Банківський акаунт</h1>
          <input
            type="text"
            name="iban"
            placeholder="Enter iban of the Bank account"
            required
            ref={ibanRef}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Орендар</h1>
          <Select onValueChange={(value) => setTennantId(Number.parseInt(value))}>
            <SelectTrigger className="w-full">
              <SelectValue placeholder={"Орендар"} />
            </SelectTrigger>
            <SelectContent>
              {tennants?.length && tennants.length > 0 && tennants.map((tennant, index) => (
                <SelectItem key={index} value={tennant.id.toString()}>
                  {tennant.displayString}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
          <h1 className="text-2xl">Ціна</h1>
          <input
            type="number"
            name="price"
            placeholder="Enter price of the Contract"
            required
            ref={priceRef}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Дата початку оренди</h1>
          <input
            type="date"
            name="rentFrom"
            placeholder="Enter rent-start date"
            required
            ref={rentFromRef}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Дата закінчення оренди</h1>
          <input
            type="date"
            name="rentTo"
            placeholder="Enter rent-end date"
            required
            ref={rentToRef}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <button
            type="button"
            onClick={handleSubmit}
            className="bg-blue-500 text-white py-2 px-4 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
          >
            Створити
          </button>
        </form>
      </div>
    </div>
  );
};

export default CreateContract;
