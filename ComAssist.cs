using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ComAssist" in code, svc and config file together.
public class ComAssist : IComAssist
{
    Community_AssistEntities db = new Community_AssistEntities();

    public List<GrantRequest> GetGrant(int personKey)
    {
        var grants = from g in db.GrantRequests
                     where g.PersonKey == personKey
                     select g;
        List<GrantRequest> L = new List<GrantRequest>();
        foreach(var gr in grants)
        {
            GrantRequest r = new GrantRequest();
            r.GrantRequestAmount = gr.GrantRequestAmount;
            r.GrantRequestDate = gr.GrantRequestDate;
            r.GrantRequestExplanation = gr.GrantRequestExplanation;
            r.GrantRequestKey = gr.GrantRequestKey;
            r.GrantReviews = gr.GrantReviews;
            r.GrantTypeKey = gr.GrantTypeKey;
            r.Person = gr.Person;
            r.PersonKey = gr.PersonKey;
            L.Add(r);
        }
        return L;
    }

    public int Login(string user, string password)
    {
        int key = 0;
        int result = db.usp_Login(user, password);
        if (result != -1)
        {
            key = result;
        }
        return key;
    }

    public bool RegisterUser(Person p, string password, string apt, string street, string city, string state, string zip, string phoneNumber, string workPhone )
    {

        bool result = true;
        int userKey = db.usp_Register(p.PersonLastName, p.PersonFirstName, p.PersonEmail, password, apt,
            street, city, state, zip, phoneNumber, workPhone );
        return result;
    }

    public bool SubmitRequest(GrantRequest gr)
    {
        int result = db.usp_AddRequest(gr.GrantTypeKey, gr.GrantRequestExplanation, gr.GrantRequestAmount, gr.PersonKey);
        if(result != -1)
        {
            return true;
        }else
        {
            return false;
        }
    }
}
