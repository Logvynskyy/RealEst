import { Key, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../../../@/components/ui/select";
import { Endpoints } from "../../constants/endpoints";
import CreateDefect from "./defects/CreateDefect";
import { Defect } from "../../interfaces/Defect";
import { TrashIcon } from "lucide-react";

const CreateUnit = () => {
  const unitTypes = [
    { id: 0, name: "Кімната" },
    { id: 1, name: "Квартира" },
    { id: 2, name: "Дім" },
    { id: 3, name: "Гараж" },
    { id: 4, name: "Склад" },
  ];

  const [unitType, setUnitType] = useState<number>(0);
  const [isOpen, setIsOpen] = useState(false);
  const [defects, setDefects] = useState<Defect[]>([]);
  const navigate = useNavigate();
  const nameRef = useRef<HTMLInputElement>(null);
  const addressRef = useRef<HTMLInputElement>(null);
  const footageRef = useRef<HTMLInputElement>(null);

  const handleSubmit = async () => {
    try {
      const name = nameRef.current?.value;
      const address = addressRef.current?.value;
      const footage = footageRef.current?.value;

      const response = await fetch("https://localhost:7115/api/Units/create", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("JWTToken"),
        },
        body: JSON.stringify({ id: 0, name, address, unitType, footage, defects }),
      });

      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      try {
        navigate(Endpoints.units);
      } catch (error) {
        console.error("Redirect error:", error);
      }
    } catch (error) {
      console.error("Creating error:", error);
    }
  };

  const handleDefectSubmit = (data: Defect) => {
    console.log("Submitted data:", data);

    setDefects((prevData: any) => [...prevData, data]);

    setIsOpen(false);
  };

  const deleteDefect = async (id: number) => {

      setDefects((defects) => defects?.filter((defect) => defect.id != id));

      return [];
  };

  return (
    <div className="w-full flex min-h-screen justify-center items-center bg-[#6DDCFF]">
      <div className="bg-white p-8 rounded-lg shadow-md w-[30rem]">
        <form className="flex flex-col gap-4">
          <h1 className="text-2xl">Назва</h1>
          <input
            type="text"
            name="name"
            placeholder="Enter name of the Unit"
            required
            ref={nameRef}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Адреса</h1>
          <input
            type="text"
            name="address"
            placeholder="Enter address of the Unit"
            required
            ref={addressRef}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Тип об'єкту</h1>
          <Select
            onValueChange={(value) => setUnitType(Number.parseInt(value))}
          >
            <SelectTrigger className="w-full">
              <SelectValue placeholder={"Тип об'єкту"} />
            </SelectTrigger>
            <SelectContent>
              {unitTypes.map((type, index) => (
                <SelectItem key={index} value={type.id.toString()}>
                  {type.name}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
          <h1 className="text-2xl">Метраж</h1>
          <input
            type="number"
            name="footage"
            placeholder="Enter footage of the Unit"
            required
            ref={footageRef}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Дефекти</h1>
          <CreateDefect
            isOpen={isOpen}
            onClose={() => setIsOpen(false)}
            onSubmit={handleDefectSubmit}
          />
          {defects.length > 0 && (
            <div>
              {defects.map((item: any, index: Key | null | undefined) => (
                <div key={index} className="flex">
                  <div>{item.name}</div>
                  <div
                  className="bg-white cursor-pointer"
                  onClick={() => deleteDefect(item.id)}
                >
                  <TrashIcon className="stroke-black" />
                </div>
                </div>
              ))}
            </div>
          )}
          <button type="button" onClick={() => setIsOpen(true)}>Додати дефект</button>
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

export default CreateUnit;
