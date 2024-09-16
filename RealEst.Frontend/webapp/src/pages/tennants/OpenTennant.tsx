import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Tennant } from "../../interfaces/Tennant";
import { MailIcon } from "lucide-react";

const OpenTennant = () => {
  const { tennantId } = useParams<{ tennantId: string }>();
  const [tennant, setTennants] = useState<Tennant>();

  const getTennantById = async () => {
    try {
      const response = await fetch(
        "https://localhost:7115/api/Tennants/" + tennantId,
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

      setTennants(responseData);
    } catch (error) {
      console.error("Error getting tennant:", error);
      return [];
    }
  };

  useEffect(() => {
    getTennantById();
  }, []);

  return (
    <div className="w-full flex min-h-screen justify-center items-center bg-[#6DDCFF]">
      <div className="bg-white p-8 rounded-lg shadow-md w-[30rem]">
        <h1 className="text-2xl">Ім'я - {tennant?.name}</h1>
        <h1 className="text-2xl">Прізвище - {tennant?.lastName}</h1>
        <h1 className="flex text-2xl">Електронна пошта - <a href={`mailto:${tennant?.email}`} className="underline">{tennant?.email}</a><MailIcon/></h1>
        {tennant?.debt && tennant?.debt > 0 && (
            <h1 className="text-2xl">Борг - {tennant.debt}</h1>
        )}
      </div>
    </div>
  );
};

export default OpenTennant;
