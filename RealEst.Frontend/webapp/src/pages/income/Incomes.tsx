import { useEffect, useState } from "react";
import { BarChart, Bar, Rectangle, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';
import { Income } from "../../interfaces/Income";

const Incomes = () => {
  const [income, setIncome] = useState<Income[]>();

  const getIncome = async () => {
    try {
      const response = await fetch("https://localhost:7115/api/Contracts/income", {
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

      setIncome(responseData);
    } catch (error) {
      console.error("Error getting income:", error);
      return [];
    }
  };

  useEffect(() => {
    getIncome();
  }, []);

  return (
    <div className="flex w-full min-h-screen p-10 justify-center bg-[#6DDCFF]">
      <div className="w-full p-2 bg-white gap-y-4 relative">
        <div className="flex flex-col justify-items-start p-2 bg-white gap-y-4">
          <BarChart
            width={1400}
            height={800}
            data={income}
            margin={{
              top: 5,
              right: 30,
              left: 20,
              bottom: 5,
            }}
          >
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="month" />
            <YAxis />
            <Tooltip />
            <Legend />
            <Bar dataKey="amount" fill="#8884d8" activeBar={<Rectangle fill="pink" stroke="blue" />} />
          </BarChart>
        </div>
      </div>
    </div>
  );
};

export default Incomes;
