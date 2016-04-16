using UnityEngine;
using System.Collections;

public interface ITriggerEvent
{
    void Remove(string name);
    void Selection(string selname);
    void Rename(string oldname, string newname);
    void Link(string root, string target);
}
