import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Unit } from "../../interfaces/Unit";

const OpenUnit = () => {
  const { unitId } = useParams<{ unitId: string }>();
  const [unit, setUnits] = useState<Unit>();

  const unitTypes = [
    { id: 0, name: "Кімната" },
    { id: 1, name: "Квартира" },
    { id: 2, name: "Дім" },
    { id: 3, name: "Гараж" },
    { id: 3, name: "Склад" },
  ];

  const getUnitById = async () => {
    try {
      const response = await fetch(
        "https://localhost:7115/api/Units/" + unitId,
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + localStorage.getItem("JWTToken"),
          },
        }
      );

      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      const responseData = await response.json();
      console.log(responseData);

      setUnits(responseData);
    } catch (error) {
      console.error("Error getting unit:", error);
      return [];
    }
  };

  useEffect(() => {
    getUnitById();
  }, []);

  return (
    <div className="w-full flex min-h-screen justify-center items-center bg-[#6DDCFF]">
      <div className="bg-white p-8 rounded-lg shadow-md w-[30rem]">
        <h1 className="text-2xl">Назва - {unit?.name}</h1>
        <h1 className="text-2xl">Адреса - {unit?.address}</h1>
        <h1 className="text-2xl">
          Тип об'єкту - {unitTypes[unit?.unitType ?? 0].name}
        </h1>
        <h1 className="text-2xl">Метраж - {unit?.footage}</h1>
        {unit?.defects && unit?.defects.length > 0 && (
          <div>
            <h1 className="text-2xl">Дефекти:</h1>
            <div>
              {unit?.defects.map((item, index) => (
                <div key={index} className="text-2xl">
                  <div>{item.name}</div>
                </div>
              ))}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default OpenUnit;
