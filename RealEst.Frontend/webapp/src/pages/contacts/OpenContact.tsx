import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Contact } from "../../interfaces/Contact";
import { MailIcon, PhoneIcon } from "lucide-react";

const OpenContact = () => {
  const { contactId } = useParams<{ contactId: string }>();
  const [contact, setContact] = useState<Contact>();

  const contactTypes = [
    { id: 0, name: "ФОП" },
    { id: 1, name: "Фірма" },
    { id: 2, name: "Сайт з аренди нерухомості" }
  ];

  const getContactById = async () => {
    try {
      const response = await fetch(
        "https://localhost:7115/api/Contacts/" + contactId,
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

      setContact(responseData);
    } catch (error) {
      console.error("Error getting contact:", error);
      return [];
    }
  };

  useEffect(() => {
    getContactById();
  }, []);

  return (
    <div className="w-full flex min-h-screen justify-center items-center bg-[#6DDCFF]">
      <div className="bg-white p-8 rounded-lg shadow-md w-[30rem]">
        <h1 className="text-2xl">Ім'я - {contact?.name}</h1>
        <h1 className="text-2xl">Прізвище - {contact?.lastName}</h1>
        <h1 className="flex text-2xl">Електронна пошта - <a href={`mailto:${contact?.email}`} className="underline">{contact?.email}</a><MailIcon/></h1>
        <h1 className="flex text-2xl">Номер телефону - <a href={`tel:${contact?.phoneNumber}`} className="underline">{contact?.phoneNumber}</a><PhoneIcon/></h1>
        <h1 className="text-2xl">
          Тип контакту - {contactTypes[contact?.contactType ?? 0].name}
        </h1>
        <h1 className="text-2xl">Пріоритет - {contact?.priority}</h1>
      </div>
    </div>
  );
};

export default OpenContact;
